using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;
using Humanizer;
using System;
using System.Text;
using json2record.common.Exceptions;
using System.Linq;

namespace json2record.common.Services {
    public class JsonParserService {
        private readonly UTF8Encoding _encoding;
        public JsonParserService() {
            _encoding = new UTF8Encoding(true);
        }

        public FileModel Parse(
            StreamReader streamReader,
            string recordName,
            ref Dictionary<string, FileModel> files,
            bool tryToHandleNonMatchingDuplicates = false) {

            var readAttributeName = true;
            string currentAttributeName = "";
            string currentValue = "";

            bool isInsideList = false;
            bool enclosedInQuotes = false;
            
            var currentFile = new FileModel()
            {
                name = String.IsNullOrWhiteSpace(currentAttributeName) ? recordName : currentAttributeName,
                attributes = new HashSet<AttributeModel>(),
                packages = new HashSet<string>()
            };

            int cInt;
            char cChar;
            while ((cInt = streamReader.Read()) != -1)
            {
                cChar = (char) cInt;
                switch (cChar) {
                    case '{':
                        // If subJSON already registered
                        var subRecordName = String.IsNullOrWhiteSpace(currentAttributeName) ? recordName : currentAttributeName;
                        if(files.ContainsKey(subRecordName)) {
                            Console.WriteLine($"Found duplicate subJSON with key '{subRecordName}'.");
                            var alternateStructure = Parse(
                                streamReader,
                                subRecordName,
                                ref files,
                                tryToHandleNonMatchingDuplicates);
                            if(!(alternateStructure == files.GetValueOrDefault(subRecordName)))
                            {
                                if (tryToHandleNonMatchingDuplicates)
                                {
                                    files.Add(
                                        recordName+subRecordName.Camelize(),
                                        alternateStructure);
                                    currentFile.attributes.Add(new AttributeModel {
                                        name = recordName+subRecordName,
                                        annotatedName = subRecordName,
                                        datatype = "object",
                                        isList = isInsideList
                                    });
                                }
                                else{
                                    throw new NonMatchingDuplicateSubrecordsException(
                                        $"duplicate subJSON '{subRecordName}' in '{recordName}' did not match previously " + 
                                        $"generated record '{alternateStructure.name}'.");
                                }
                            }
                            else{
                                currentFile.attributes.Add(new AttributeModel {
                                    name = subRecordName,
                                    datatype = "object",
                                    isList = isInsideList
                                });                               
                            }
                        }
                        // If inner JSON with same name
                        else if (recordName == subRecordName){
                            files.Add(
                                $"{subRecordName}Value",
                                Parse(
                                    streamReader,
                                    subRecordName,
                                    ref files,
                                    tryToHandleNonMatchingDuplicates));
                            currentFile.attributes.Add(new AttributeModel {
                                name = $"{subRecordName}Value",
                                annotatedName = subRecordName,
                                datatype = "object",
                                isList = isInsideList
                            });
                        }
                        else {
                            files.Add(
                                subRecordName,
                                Parse(
                                    streamReader,
                                    subRecordName,
                                    ref files,
                                    tryToHandleNonMatchingDuplicates));
                            currentFile.attributes.Add(new AttributeModel {
                                name = subRecordName,
                                datatype = "object",
                                isList = isInsideList
                            });
                        }
                        break;
                    case '}':
                        // End of object
                        return currentFile;
                    case '"':
                        if (readAttributeName) {
                            char c;
                            while ((c = (char) streamReader.Read()) != '"') {
                                currentAttributeName += c;
                            }
                            readAttributeName = false;
                        }
                        else {
                            char c;
                            while ((c = (char) streamReader.Read()) != '"') { 
                                currentValue += c;
                                if (c == '\\') {
                                    currentValue += (char) streamReader.Read();
                                }
                            }

                            // Infer datatype from contents.
                            DateTime dateTimeValue;
                            if (DateTime.TryParse(currentValue, out dateTimeValue)){
                                currentFile.packages.Add("System");
                                currentFile.attributes.Add(new AttributeModel {
                                    name = currentAttributeName,
                                    datatype = "datetime",
                                    isList = isInsideList
                                });
                                currentValue = "";
                            }
                            else {
                                currentFile.attributes.Add(new AttributeModel {
                                    name = currentAttributeName,
                                    datatype = "string",
                                    isList = isInsideList
                                });
                                currentValue = "";
                            }
                        }
                        break;
                    case '[':
                        // List
                        currentFile.packages.Add("System.Collections.Generic");
                        isInsideList = true;
                        break;
                    case ']':
                        isInsideList = false;
                        break;
                    case ':':
                        readAttributeName = false;
                        break;
                    case ',':
                        if (isInsideList)
                        {
                            // Flush remaining entries of object already parsed.
                            char c;
                            var listCounter = 0;
                            while ((c = (char) streamReader.Peek()) != ']' || listCounter > 0) { 
                                c = (char) streamReader.Read();
                                if (c == '[') { listCounter++; }
                                else if (c == ']') { listCounter --; }
                            }
                        }
                        readAttributeName = true;
                        currentAttributeName = "";
                        break;
                    case '\n':
                        break;
                    default:
                        if (char.IsWhiteSpace(cChar)) {
                            break;
                        }
                        else if (!readAttributeName && char.IsLetterOrDigit(cChar)) {

                            // Read variable contents.
                            string value = cChar.ToString();
                            char c;
                            if (enclosedInQuotes) {
                                while ((c = (char) streamReader.Read()) != '"') { 
                                    value += c;
                                }
                                enclosedInQuotes = !enclosedInQuotes;

                                // Infer datatype from contents.
                                DateTime dateTimeValue;
                                if (DateTime.TryParse(value, out dateTimeValue)){
                                    currentFile.packages.Add("System");
                                    currentFile.attributes.Add(new AttributeModel {
                                        name = currentAttributeName,
                                        datatype = "datetime",
                                        isList = isInsideList
                                    });
                                }
                                else {
                                    currentFile.attributes.Add(new AttributeModel {
                                        name = currentAttributeName,
                                        datatype = "string",
                                        isList = isInsideList
                                    });
                                }
                                break;
                            }
                            else {
                                while(!(new List<char>{',', ' ', '}'}.Contains(c = (char)streamReader.Peek()))) {
                                    value += c;
                                    streamReader.Read();
                                }

                                // Infer datatype from contents.
                                bool boolValue;
                                int intValue;
                                float floatValue;
                                if (Boolean.TryParse(value, out boolValue)) {
                                    currentFile.attributes.Add(new AttributeModel {
                                        name = currentAttributeName,
                                        datatype = "boolean",
                                        isList = isInsideList
                                    });
                                }
                                else if (Int32.TryParse(value, out intValue)) {
                                    currentFile.attributes.Add(new AttributeModel {
                                        name = currentAttributeName,
                                        datatype = "integer",
                                        isList = isInsideList
                                    });
                                }
                                else if (float.TryParse(value, out floatValue)) {
                                    currentFile.attributes.Add(new AttributeModel {
                                        name = currentAttributeName,
                                        datatype = "double",
                                        isList = isInsideList
                                    });
                                }
                            }
                        }
                        break;
                }
            }
            return currentFile;
        }
    }
}
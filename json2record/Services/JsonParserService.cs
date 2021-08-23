using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;
using Humanizer;
using System;
using System.Text;

namespace json2record.Services {
    public class JsonParserService {
        private readonly UTF8Encoding _encoding;
        public JsonParserService() {
            _encoding = new UTF8Encoding(true);
        }

        public List<string> Parse(
            StreamReader streamReader,
            string document,
            string recordName,
            ref Dictionary<string, SortedSet<string>> packages,
            ref Dictionary<string, List<string>> structure) {
            
            var lines = new List<string>();
            var localPackages = new SortedSet<string>();

            var readAttributeName = true;
            string currentAttributeName = "";

            bool isInsideList = false;
            bool enclosedInQuotes = false;

            int cInt;
            char cChar;
            while ((cInt = streamReader.Read()) != -1)
            {
                cChar = (char) cInt;
                switch (cChar) {
                    case '{':
                        // Start of object
                        structure.Add(
                            string.IsNullOrEmpty(currentAttributeName) ? recordName : currentAttributeName, 
                            Parse(
                                streamReader,
                                document,
                                string.IsNullOrEmpty(currentAttributeName) ? recordName : currentAttributeName,
                                ref packages,
                                ref structure));
                        lines.Add(GenerateObjectAttribute(currentAttributeName, isInsideList));
                        break;
                    case '}':
                        // End of object
                        packages.Add(recordName, localPackages);
                        return lines;
                    case '"':
                        enclosedInQuotes = !enclosedInQuotes;
                        if (readAttributeName) {
                            char c;
                            while ((c = (char) streamReader.Peek()) != '"') {
                                currentAttributeName += c;
                                streamReader.Read();
                            }
                            readAttributeName = false;
                        }
                        break;
                    case '[':
                        // List
                        localPackages.Add("System.Collections.Generic");
                        isInsideList = true;
                        break;
                    case ']':
                        isInsideList = false;
                        break;
                    case ':':
                        readAttributeName = false;
                        break;
                    case ',':
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
                            }
                            else {
                                while(!(new List<char>{',', ' ', '}'}.Contains(c = (char)streamReader.Peek()))) {
                                    value += c;
                                    streamReader.Read();
                                }
                            }

                            // Infer datatype from contents.
                            DateTime dateTimeValue;
                            int intValue;
                            float floatValue;
                            if (DateTime.TryParse(value, out dateTimeValue)){
                                localPackages.Add("System");
                                lines.Add(GenerateDateTimeAttribute(currentAttributeName, isInsideList));
                            }
                            else if (Int32.TryParse(value, out intValue)) {
                                lines.Add(GenerateIntAttribute(currentAttributeName, isInsideList));
                            }
                            else if (float.TryParse(value, out floatValue)) {
                                lines.Add(GenerateDoubleAttribute(currentAttributeName, isInsideList));
                            }
                            else {
                                lines.Add(GenerateStringAttribute(currentAttributeName, isInsideList));
                            } 
                        }
                        break;
                }
            }
            return lines;
        }



        private string GenerateObjectAttribute(string currentField, bool isInsideList)
        {
            return isInsideList? 
                $"        public List<{currentField.Pascalize()}> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public {currentField.Pascalize()} {currentField.Camelize()} {{ get; init; }} \n";

        }

        private string GenerateStringAttribute(string currentField, bool isInsideList)
        {
            return isInsideList? 
                $"        public List<string> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public string {currentField.Camelize()} {{ get; init; }} \n";
        }
        private string GenerateIntAttribute(string currentField, bool isInsideList)
        {
            return isInsideList ? 
                $"        public List<int> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public int {currentField.Camelize()} {{ get; init; }} \n";
        }
        private string GenerateDoubleAttribute(string currentField, bool isInsideList)
        {
            return isInsideList ? 
                $"        public List<double> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public double {currentField.Camelize()} {{ get; init; }} \n";
        }

        private string GenerateDateTimeAttribute(string currentField, bool isInsideList)
        {
            return isInsideList ? 
                $"        public List<DateTime> {currentField} {{ get; init; }} \n" :
                $"        public DateTime {currentField} {{ get; init; }} \n";
        }
    }
}
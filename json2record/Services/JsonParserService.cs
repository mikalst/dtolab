using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;
using Humanizer;
using System;
using System.Text;
using json2record.Exceptions;
using System.Linq;

namespace json2record.Services {
    public class JsonParserService {
        private readonly UTF8Encoding _encoding;
        public JsonParserService() {
            _encoding = new UTF8Encoding(true);
        }

        public HashSet<string> Parse(
            StreamReader streamReader,
            string recordName,
            ref Dictionary<string, SortedSet<string>> packages,
            ref Dictionary<string, HashSet<string>> structure) {
            
            var lines = new HashSet<string>();
            var localPackages = new SortedSet<string>();

            var readAttributeName = true;
            string currentAttributeName = "";
            string currentValue = "";

            bool isInsideList = false;
            bool enclosedInQuotes = false;

            int cInt;
            char cChar;
            while ((cInt = streamReader.Read()) != -1)
            {
                cChar = (char) cInt;
                switch (cChar) {
                    case '{':
                        // If subJSON already registered
                        var subRecordName = String.IsNullOrWhiteSpace(currentAttributeName) ? recordName : currentAttributeName;
                        if(structure.ContainsKey(subRecordName)) {
                            Console.WriteLine($"Found duplicate subJSON with key '{subRecordName}'.");
                            var alternateStructure = Parse(
                                streamReader,
                                subRecordName,
                                ref packages,
                                ref structure);
                            if(!alternateStructure.SetEquals(structure.GetValueOrDefault(subRecordName)))
                            {
                                throw new NonMatchingDuplicateSubrecordsException(
                                    $"duplicate subJSON '{subRecordName}' in '{recordName}' did not match previously" + 
                                    $"generated record '{alternateStructure}'.");
                            }
                        }
                        else {
                            structure.Add(
                                subRecordName,
                                Parse(
                                    streamReader,
                                    subRecordName,
                                    ref packages,
                                    ref structure));
                        }
                        lines.Add(GenerateObjectAttribute(subRecordName, isInsideList));
                        break;
                    case '}':
                        // End of object
                        packages.TryAdd(recordName, localPackages);
                        return lines;
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
                                localPackages.Add("System");
                                lines.Add(GenerateDateTimeAttribute(currentAttributeName, isInsideList));
                                currentValue = "";
                            }
                            else {
                                lines.Add(GenerateStringAttribute(currentAttributeName, isInsideList));
                                currentValue = "";
                            }
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
                                    localPackages.Add("System");
                                    lines.Add(GenerateDateTimeAttribute(currentAttributeName, isInsideList));
                                }
                                else {
                                    lines.Add(GenerateStringAttribute(currentAttributeName, isInsideList));
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
                                    lines.Add(GenerateBooleanAttribute(currentAttributeName, isInsideList));
                                }
                                else if (Int32.TryParse(value, out intValue)) {
                                    lines.Add(GenerateIntAttribute(currentAttributeName, isInsideList));
                                }
                                else if (float.TryParse(value, out floatValue)) {
                                    lines.Add(GenerateDoubleAttribute(currentAttributeName, isInsideList));
                                }
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
        private string GenerateBooleanAttribute(string currentField, bool isInsideList)
        {
            return isInsideList ? 
                $"        public List<bool> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public bool {currentField.Camelize()} {{ get; init; }} \n";
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
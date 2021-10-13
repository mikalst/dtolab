using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Humanizer;
using json2record.common;

namespace json2record.Services {
    class FileWriterService {
        public FileWriterService() {
        }

        public bool WriteFile(
            string key,
            ParsedArgs parsedArgs, 
            List<string> filePackages, 
            List<AttributeModel> attributes) {
            var output = "";
            foreach (var s in filePackages) {
                output += $"using {s}; \n";
            }
            if (filePackages.Count > 0) output += "\n"; 
            output += $"namespace {parsedArgs.namespaceArg} {{ \n";
            var inner = GenerateDocument(key, attributes);
            output += inner;
            output += "}";
            var filePath = Path.Combine(parsedArgs.outputDirectory, key.Pascalize() + ".cs");

            using (FileStream fs = File.Create(filePath))
            {
                byte[] o = new UTF8Encoding(true).GetBytes(output);
                fs.Write(o, 0, o.Length);
            }
            Console.WriteLine($"Successfully created file {filePath}!");
            return true;
        }

        private string GenerateDocument(string recordName, List<AttributeModel> attributeModels)
        {
            var innerDocument = "";
            innerDocument += $"    public record {recordName.Pascalize()} {{ \n";
            foreach(var l in attributeModels) {
                switch (l.datatype) {
                    case "datetime":
                        innerDocument += GenerateDateTimeAttribute(l.name, l.isList);
                        break;
                    case "object":
                        innerDocument += GenerateObjectAttribute(l.name, l.isList);
                        break;
                    case "boolean":
                        innerDocument += GenerateBooleanAttribute(l.name, l.isList); 
                        break;
                    case "string":
                        innerDocument += GenerateStringAttribute(l.name, l.isList); 
                        break;
                    case "double":
                        innerDocument += GenerateDoubleAttribute(l.name, l.isList); 
                        break;
                    case "integer":
                        innerDocument += GenerateIntAttribute(l.name, l.isList); 
                        break;
                    default:
                        break;
                }
            }
            innerDocument += $"    }} \n";
            return innerDocument;
        }

        private string GenerateObjectAttribute(string currentField, bool isList)
        {
            return isList? 
                $"        public List<{currentField.Pascalize()}> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public {currentField.Pascalize()} {currentField.Camelize()} {{ get; init; }} \n";

        }

        private string GenerateStringAttribute(string currentField, bool isList)
        {
            return isList? 
                $"        public List<string> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public string {currentField.Camelize()} {{ get; init; }} \n";
        }
        private string GenerateIntAttribute(string currentField, bool isList)
        {
            return isList ? 
                $"        public List<int> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public int {currentField.Camelize()} {{ get; init; }} \n";
        }
        private string GenerateBooleanAttribute(string currentField, bool isList)
        {
            return isList ? 
                $"        public List<bool> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public bool {currentField.Camelize()} {{ get; init; }} \n";
        }
        private string GenerateDoubleAttribute(string currentField, bool isList)
        {
            return isList ? 
                $"        public List<double> {currentField.Camelize()} {{ get; init; }} \n" :
                $"        public double {currentField.Camelize()} {{ get; init; }} \n";
        }

        private string GenerateDateTimeAttribute(string currentField, bool isList)
        {
            return isList ? 
                $"        public List<DateTime> {currentField} {{ get; init; }} \n" :
                $"        public DateTime {currentField} {{ get; init; }} \n";
        }
    }
}
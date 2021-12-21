using System.Collections.Generic;
using System.Linq;
using Humanizer;
using dtolab.common.Constans;

namespace dtolab.common.Services {

    public class CSharpGeneratorService {
        public string GenerateDocument(
            string recordName,
            HashSet<AttributeModel> attributeModels,
            HashSet<string> packages,
            string namespaceValue,
            string classType)
        {
            var output = $"namespace {namespaceValue} {{ \n";

            output += $"    public {classType} {recordName.Pascalize()} {{ \n";

            foreach(var l in attributeModels) {

                var propertyName = l.name;
                if (l.annotatedName != null) {
                    propertyName = l.name;
                    output += $"        [JsonPropertyName(\"{l.annotatedName}\")]\n";
                    packages.Add("System.Text.Json.Serialization");
                }
                else if (ReservedCSharpKeywords.Keywords.Contains(l.name)) {
                    propertyName = l.name + "Value";
                    output += $"        [JsonPropertyName(\"{l.name}\")]\n";
                    packages.Add("System.Text.Json.Serialization");
                }
                else {
                    propertyName = new string(
                        l.name.Where(
                            c => char.IsLetterOrDigit(c) || c == '_').ToArray());
                    if (propertyName.Length < l.name.Length)
                    {
                        output += $"        [JsonPropertyName(\"{l.name}\")]\n";
                        packages.Add("System.Text.Json.Serialization");
                    } 
                }

                switch (l.datatype) {
                    case "datetime":
                        output += GenerateDateTimeAttribute(propertyName, l.isList);
                        break;
                    case "object":
                        output += GenerateObjectAttribute(propertyName, l.isList);
                        break;
                    case "boolean":
                        output += GenerateBooleanAttribute(propertyName, l.isList); 
                        break;
                    case "string":
                        output += GenerateStringAttribute(propertyName, l.isList); 
                        break;
                    case "double":
                        output += GenerateDoubleAttribute(propertyName, l.isList); 
                        break;
                    case "integer":
                        output += GenerateIntAttribute(propertyName, l.isList); 
                        break;
                    default:
                        break;
                }
            }
            
            var packagesSectionOutputString = ""; 
            foreach (var s in packages) {
                packagesSectionOutputString += $"using {s}; \n";
            }
            if (packages.Count > 0) packagesSectionOutputString += "\n"; 

            var result = packagesSectionOutputString + output + $"    }}\n}} \n";

            return result;
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

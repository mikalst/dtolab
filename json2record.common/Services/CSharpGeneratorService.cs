using System.Collections.Generic;
using Humanizer;

namespace json2record.common.Services {

    public class CSharpGeneratorService {
        public string GenerateDocument(string recordName, List<AttributeModel> attributeModels)
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

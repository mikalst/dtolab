using System.Collections.Generic;

namespace json2record.common {
    public record FileModel {
        public string name { get; init; }
        public List<AttributeModel> attributes { get; init; }
        public List<string> packages { get; init; } 
    }
}
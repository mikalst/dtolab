using System.Collections.Generic;

namespace json2record.common {
    public record AttributeModel {
        public string name { get; init; }
        public string datatype { get; init; }
        public bool isList { get; init; }
    }
}
using System.Collections.Generic;

namespace dtolab.common {
    public record AttributeModel {
        public string name { get; init; }
        public string annotatedName { get; init; } = null;
        public string datatype { get; init; }
        public bool isList { get; init; }
    }
}
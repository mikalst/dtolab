using System.Collections.Generic;
using json2record.common;

namespace json2record.func.DTOs {
    public record OutputDTO {
        public string name { get; init; }
        public Dictionary<string, FileModel> files { get; init; }
    }
}
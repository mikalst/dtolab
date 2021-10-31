using System.Collections.Generic;
using json2record.common;

namespace json2record.func.DTOs {
    public record FileOutputDTO {
        public string name { get; init; }
        public Dictionary<string, string> files { get; init; }
    }
}
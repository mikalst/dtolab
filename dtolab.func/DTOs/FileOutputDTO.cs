using System.Collections.Generic;
using dtolab.common;

namespace dtolab.func.DTOs {
    public record FileOutputDTO {
        public string name { get; init; }
        public Dictionary<string, string> files { get; init; }
    }
}
using System.Collections.Generic;
using dtolab.common;

namespace dtolab.func.DTOs {
    public record OutputDTO {
        public string name { get; init; }
        public Dictionary<string, FileModel> files { get; init; }
    }
}
using System.Collections.Generic; 
using System.Text.Json.Serialization; 

namespace dtolab.tests.sample6.DTOs { 
    public class Confirmations { 
        [JsonPropertyName("$type")]
        public string type { get; init; } 
        public List<string> confirmationValues { get; init; } 
    }
} 

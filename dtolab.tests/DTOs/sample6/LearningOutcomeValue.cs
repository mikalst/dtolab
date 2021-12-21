using System.Text.Json.Serialization; 

namespace dtolab.tests.sample6.DTOs { 
    public class LearningOutcomeValue { 
        [JsonPropertyName(".comwledge")]
        public string comwledge { get; init; } 
    }
} 

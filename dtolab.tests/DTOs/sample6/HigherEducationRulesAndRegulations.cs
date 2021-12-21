using System.Collections.Generic; 
using System.Text.Json.Serialization; 

namespace dtolab.tests.sample6.DTOs { 
    public class HigherEducationRulesAndRegulations { 
        public string linksToRules { get; init; } 
        [JsonPropertyName("OtherInformation")]
        public HigherEducationRulesAndRegulationsOtherInformation higherEducationRulesAndRegulationsOtherInformation { get; init; } 
        public string isPublishedLastChange { get; init; } 
        public string linksToRulesLastChange { get; init; } 
        public string documentsLastChange { get; init; } 
    }
} 

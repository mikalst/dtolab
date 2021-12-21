using System.Collections.Generic; 
using System.Text.Json.Serialization; 

namespace dtolab.tests.sample6.DTOs { 
    public class VocationalCompanyRulesAndRegulations { 
        public bool isPublished { get; init; } 
        public string linksToRules { get; init; } 
        public List<Documents> documents { get; init; } 
        [JsonPropertyName("OtherInformation")]
        public VocationalCompanyRulesAndRegulationsOtherInformation vocationalCompanyRulesAndRegulationsOtherInformation { get; init; } 
        public string isPublishedLastChange { get; init; } 
        public string linksToRulesLastChange { get; init; } 
        public string documentsLastChange { get; init; } 
    }
} 

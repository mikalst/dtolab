using System.Text.Json.Serialization; 

namespace dtolab.tests.sample6.DTOs { 
    public class LearningOutcome { 
        [JsonPropertyName("learningOutcome")]
        public LearningOutcomeValue learningOutcomeValue { get; init; } 
        public EducationContent educationContent { get; init; } 
    }
} 

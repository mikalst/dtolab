using System.Text.Json.Serialization; 

namespace dtolab.tests.sample6.DTOs { 
    public class EducationAdmissionRequirements { 
        [JsonPropertyName("educationAdmissionRequirements")]
        public EducationAdmissionRequirementsValue educationAdmissionRequirementsValue { get; init; } 
    }
} 

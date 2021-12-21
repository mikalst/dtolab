using System.Collections.Generic; 

namespace dtolab.tests.sample6.DTOs { 
    public class Education { 
        public string mainName { get; init; } 
        public string fkfMainCategoryId { get; init; } 
        public string fkfSubCategoryId { get; init; } 
        public string reasonForName { get; init; } 
        public List<string> educationGivenOn { get; init; } 
        public List<string> studyPlaces { get; init; } 
    }
} 

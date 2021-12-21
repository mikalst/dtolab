using System.Collections.Generic; 

namespace dtolab.tests.sample6.DTOs { 
    public class StudentsLearningEnvironmentAndRight { 
        public List<StudentDemocracyFiles> studentDemocracyFiles { get; init; } 
        public List<LearningEnvironmentFiles> learningEnvironmentFiles { get; init; } 
        public string studentDemocracyDescription { get; init; } 
        public string learningEnvironmentDescription { get; init; } 
        public string studentDemocracyFilesLastChange { get; init; } 
        public string learningEnvironmentFilesLastChange { get; init; } 
        public string studentDemocracyDescriptionLastChange { get; init; } 
        public string learningEnvironmentDescriptionLastChange { get; init; } 
        public string learningEnvironmentCommitteeLastChange { get; init; } 
    }
} 

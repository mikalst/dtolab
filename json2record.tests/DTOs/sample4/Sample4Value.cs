using System.Collections.Generic; 

namespace json2record.tests.sample4.DTOs { 
    public class Sample4Value { 
        public int project { get; init; } 
        public string projectCD { get; init; } 
        public List<Lines> lines { get; init; } 
        public string lastCheck { get; init; } 
    }
} 

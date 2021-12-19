using System.Collections.Generic; 
using System; 

namespace dtolab.tests.sample1.DTOs { 
    public class Sample1Value { 
        public string firstName { get; init; } 
        public string surname { get; init; } 
        public List<Address> address { get; init; } 
        public Account account { get; init; } 
        public DateTime Date { get; init; } 
    }
} 

using System; 
using System.Collections.Generic; 

namespace json2record.tests.sample1.DTOs { 
    public record Sample1 { 
        public string firstName { get; init; } 
        public string surname { get; init; } 
        public List<Address> address { get; init; } 
        public Account account { get; init; } 
        public DateTime Date { get; init; } 
    } 
}
using System; 
using System.Collections.Generic; 

namespace json2record.DTOs { 
    public record Sample { 
        public string firstName { get; init; } 
        public string surname { get; init; } 
        public List<Address> address { get; init; } 
        public Account account { get; init; } 
        public DateTime Date { get; init; } 
    } 
}
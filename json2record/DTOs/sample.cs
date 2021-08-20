using System; 
using System.Collections.Generic; 

namespace json2record.DTOs { 
    public record Address { 
        public int postalCode { get; init; } 
        public string value { get; init; } 
        public string city { get; init; } 
    } 
    public record Account { 
        public float balance { get; init; } 
        public float creditLimit { get; init; } 
        public int tier { get; init; } 
    } 
    public record Sample { 
        public string firstName { get; init; } 
        public string surname { get; init; } 
        public List<Address> address { get; init; } 
        public Account account { get; init; } 
        public DateTime Date { get; init; } 
    } 
}
using System; 
using System.Collections.Generic; 

namespace json2record.tests.sample2.DTOs { 
    public class SalesSubaccount { 
        public string subaccountNumber { get; init; } 
        public int subaccountId { get; init; } 
        public string description { get; init; } 
        public DateTime lastModifiedDateTime { get; init; } 
        public bool active { get; init; } 
        public List<Segments> segments { get; init; } 
    }
} 

namespace json2record.tests.sample2.DTOs { 
    public record CustomerLedgerAccount { 
        public string type { get; init; } 
        public string number { get; init; } 
        public string description { get; init; } 
    } 
}
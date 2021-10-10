namespace json2record.tests.sample2.DTOs { 
    public record SalesNonTaxableAccount { 
        public string type { get; init; } 
        public string number { get; init; } 
        public string description { get; init; } 
    } 
}
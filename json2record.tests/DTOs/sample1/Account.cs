namespace json2record.tests.sample1.DTOs { 
    public record Account { 
        public double balance { get; init; } 
        public double creditLimit { get; init; } 
        public int tier { get; init; } 
    } 
}
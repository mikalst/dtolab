namespace json2record.tests.sample2.DTOs { 
    public record MainAddress { 
        public int addressId { get; init; } 
        public Country country { get; init; } 
    } 
}
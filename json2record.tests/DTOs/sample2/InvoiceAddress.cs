namespace json2record.tests.sample2.DTOs { 
    public record InvoiceAddress { 
        public int addressId { get; init; } 
        public Country country { get; init; } 
    } 
}
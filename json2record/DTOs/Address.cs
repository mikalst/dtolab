namespace json2record.DTOs { 
    public record Address { 
        public int postalCode { get; init; } 
        public string value { get; init; } 
        public string city { get; init; } 
    } 
}
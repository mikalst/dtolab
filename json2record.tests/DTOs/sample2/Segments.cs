namespace json2record.tests.sample2.DTOs { 
    public record Segments { 
        public int segmentId { get; init; } 
        public string segmentDescription { get; init; } 
        public string segmentValue { get; init; } 
        public string segmentValueDescription { get; init; } 
    } 
}
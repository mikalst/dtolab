namespace json2record.common {
    public record LineModel {
        public string name { get; init; }
        public string dataDype { get; init; }
        public bool isInsideList { get; init; } 
    }
}
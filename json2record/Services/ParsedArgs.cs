namespace json2record.Services
{
    public record ParsedArgs
    {
        internal string outputDirectory;

        public string recordName { get; init; }

        public string resolvedInputPath { get; init; }
        public string outputPath { get; init; }
        public string namespaceArg { get; init; }
    }
}
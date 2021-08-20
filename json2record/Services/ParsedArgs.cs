namespace JsonToRecord.Services
{
    public record ParsedArgs
    {
        public string recordName { get; init; }

        public string resolvedInputPath { get; init; }
        public string outputPath { get; init; }
        public string namespaceArg { get; init; }
    }
}
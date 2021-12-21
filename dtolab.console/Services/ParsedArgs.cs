namespace dtolab.Services
{
    public record ParsedArgs
    {
        internal string outputDirectory;

        public string recordName { get; init; }

        public string resolvedInputPath { get; init; }
        public string outputPath { get; init; }
        public string namespaceArg { get; init; }
        public bool consolidateSubRecords { get; init; }
        public string classType { get; init; }
    }
}
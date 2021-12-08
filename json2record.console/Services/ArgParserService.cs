using System;
using System.IO;
using System.Linq;

namespace json2record.Services{
    public class ArgParserService{
        public ParsedArgs Parse(string[] args) {
            
            var directory = Directory.GetCurrentDirectory();
            var resolvedInputPath = Path.GetFullPath(new Uri(Path.Combine(directory, args[0])).LocalPath);
            var recordName = Path.GetFileName(resolvedInputPath).Split(".")[0];
            var namespaceArg = args[2];

            return new ParsedArgs {
                resolvedInputPath = resolvedInputPath,
                recordName = recordName,
                outputPath = Path.Combine(Path.GetFullPath(new Uri(Path.Combine(directory, args[1])).LocalPath), recordName + ".cs"),
                outputDirectory = Path.GetFullPath(new Uri(Path.Combine(directory, args[1])).LocalPath),
                namespaceArg = namespaceArg,
                classType = args.ElementAtOrDefault(3) ?? "class"
            };
        }
    }
}
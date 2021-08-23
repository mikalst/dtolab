using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Humanizer;

namespace json2record.Services {
    class FileWriterService {
        public FileWriterService() {
        }

        public bool WriteFile(string key, ParsedArgs parsedArgs, SortedSet<string> filePackages, List<string> fileLines) {
            var output = "";
            foreach (var s in filePackages) {
                output += $"using {s}; \n";
            }
            if (filePackages.Count > 0) output += "\n"; 
            output += $"namespace {parsedArgs.namespaceArg} {{ \n";
            var inner = GenerateDocument(key, fileLines);
            output += inner;
            output += "}";
            var filePath = Path.Combine(parsedArgs.outputDirectory, key.Pascalize() + ".cs");

            using (FileStream fs = File.Create(filePath))
            {
                byte[] o = new UTF8Encoding(true).GetBytes(output);
                fs.Write(o, 0, o.Length);
            }
            Console.WriteLine($"Successfully created file {filePath}!");
            return true;
        }

        private string GenerateDocument(string recordName, List<string> lines)
        {
            var innerDocument = "";
            innerDocument += $"    public record {recordName.Pascalize()} {{ \n";
            foreach(var s in lines) {
                innerDocument += s;
            }
            innerDocument += $"    }} \n";
            return innerDocument;
        }
    }
}
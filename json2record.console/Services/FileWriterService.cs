using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Humanizer;
using json2record.common;
using json2record.common.Services;

namespace json2record.Services {
    class FileWriterService {
        public FileWriterService() {
        }

        public bool WriteFile(
            string key,
            ParsedArgs parsedArgs, 
            HashSet<string> filePackages, 
            HashSet<AttributeModel> attributes) {

            var output = (new CSharpGeneratorService()).GenerateDocument(key, attributes, filePackages, parsedArgs.namespaceArg);
            var filePath = Path.Combine(parsedArgs.outputDirectory, key.Pascalize() + ".cs");

            using (FileStream fs = File.Create(filePath))
            {
                byte[] o = new UTF8Encoding(true).GetBytes(output);
                fs.Write(o, 0, o.Length);
            }
            Console.WriteLine($"Successfully created file {filePath}!");
            return true;
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Humanizer;
using dtolab.common;
using dtolab.common.Services;

namespace dtolab.Services {
    class FileWriterService {
        public FileWriterService() {
        }

        public bool WriteFile(
            string key,
            ParsedArgs parsedArgs, 
            HashSet<AttributeModel> attributes,
            HashSet<string> packages) {

            var output = (new CSharpGeneratorService()).GenerateDocument(
                key, 
                attributes, 
                packages, 
                parsedArgs.namespaceArg,
                parsedArgs.classType);

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
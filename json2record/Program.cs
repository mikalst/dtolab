using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Humanizer;
using json2record.Services;

namespace JsonToRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyInfo = Assembly.GetEntryAssembly();
            var assemblyVersion = assemblyInfo
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion
                .ToString();

            if (args.Length == 3)
            {
                var parsedArgs = new ArgParserService().Parse(args);

                Console.WriteLine($"json2record command line tool, json2record-v{assemblyVersion}");
                Console.WriteLine($"Visit https://github.com/json2record/json2record for more information.");
                Console.WriteLine($"json2record bin directory {assemblyInfo.Location}");
;
                Console.WriteLine($"Generating record for {parsedArgs.resolvedInputPath}");
                Console.WriteLine($"      with target {parsedArgs.outputPath}");

                try
                {
                    var structure = new Dictionary<string, List<string>>();
                    var packages = new Dictionary<string, SortedSet<string>>();
                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(parsedArgs.resolvedInputPath))
                    {
                        var lines = new JsonParserService().Parse(sr, "", parsedArgs.recordName, ref packages, ref structure);

                    }

                    var fileWriterService = new FileWriterService();
                    foreach (var key in structure.Keys) {
                        // Create the file, or overwrite if the file exists.
                        fileWriterService.WriteFile(key, parsedArgs, packages[key], structure[key]);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }



                return;
            }
            else {
                Console.WriteLine("[ERROR] Program takes three and exactly two input parameters.");
                Console.WriteLine($"writefile v{assemblyVersion}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                Console.WriteLine("json2record <filePath> <outputDirectory> <namespace>");

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using dtolab.common;
using dtolab.common.Exceptions;
using dtolab.common.Services;
using dtolab.Services;

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

            if (args.Length>=3 && args.Length<=4)
            {
                var parsedArgs = new ArgParserService().Parse(args);

                Console.WriteLine($"dtolab command line tool, dtolab-v{assemblyVersion}");
                Console.WriteLine($"Visit https://github.com/mikalst/dtolab for more information.");
                Console.WriteLine($"dtolab bin directory {assemblyInfo.Location}");
;
                Console.WriteLine($"Generating record for {parsedArgs.resolvedInputPath}");
                Console.WriteLine($"... with target {parsedArgs.outputPath}");

                try
                {
                    var files = new Dictionary<string, FileModel>();
                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(parsedArgs.resolvedInputPath))
                    {
                        var spec = new JsonParserService().Parse(
                            sr,
                            parsedArgs.recordName,
                            ref files);
                    }

                    var fileWriterService = new FileWriterService();
                    foreach (var key in files.Keys) {
                        // Create the file, or overwrite if the file exists.
                        fileWriterService.WriteFile(
                            key,
                            parsedArgs,
                            files[key].attributes,
                            files[key].packages);
                    }
                }
                catch (NonMatchingDuplicateSubrecordsException nmex)
                {
                    Console.WriteLine(nmex.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return;
            }
            else {
                Console.WriteLine("[ERROR] Program takes three or four input parameters.");
                Console.WriteLine($"dtolab v{assemblyVersion}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                Console.WriteLine("dtolab <filePath> <outputDirectory> <namespace> [classType]");

            }
        }
    }
}

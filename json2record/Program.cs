using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using JsonToRecord.Services;

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
                    var output = "";
                    var inner = "";
                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(parsedArgs.resolvedInputPath))
                    {
                        SortedSet<string> packages = new SortedSet<string>();
                        inner = new JsonParserService().Parse(sr, "", parsedArgs.recordName, ref packages);

                        foreach (var s in packages){
                            output += $"using {s}; \n";
                        }
                        if (packages.Count > 0) output += "\n"; 
                        output += $"namespace {parsedArgs.namespaceArg} {{ \n";
                        output += inner;
                        output += "}";
                    }

                    // Create the file, or overwrite if the file exists.
                    using (FileStream fs = File.Create(parsedArgs.outputPath))
                    {
                        byte[] o = new UTF8Encoding(true).GetBytes(output);
                        fs.Write(o, 0, o.Length);
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

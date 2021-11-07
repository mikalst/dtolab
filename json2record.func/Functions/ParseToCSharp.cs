using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using json2record.common;
using json2record.common.Services;
using json2record.func.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace json2record.func
{
    public static class ParseToCSharp
    {
        [FunctionName("csharp")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ExecutionContext executionContext,
            ILogger log)
        {
            var name = "dto";

            var files = new Dictionary<string, FileModel>();

            FileModel file;
            using (var sr = new StreamReader(req.Body))
                {
                    file = new JsonParserService().Parse(
                        new StreamReader(req.Body),
                        name,
                        ref files
                );
            }
            
            var dto = new FileOutputDTO() {
                name = name,
                files = new Dictionary<string, string>()
            };

            foreach(var key in files.Keys)
            {
                var innerFile = files[key];
                var output = "";
                foreach (var s in innerFile.packages) {
                    output += $"using {s}; \n";
                }
                if (innerFile.packages.Count > 0) output += "\n"; 

                output += $"namespace dto {{ \n";
                var inner = (new CSharpGeneratorService()).GenerateDocument(key, innerFile.attributes);
                output += inner;
                output += "}";
                dto.files.Add(key, output);
            }

            log.LogInformation("C# HTTP trigger successfully processed a request.");

            return Task.FromResult<IActionResult>(new OkObjectResult(dto));
        }
    }
}

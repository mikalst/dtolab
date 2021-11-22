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
        public async static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ExecutionContext executionContext,
            ILogger log)
        {
            var name = req.Query["name"];
            var ns = req.Query["ns"];
            var classtype = req.Query["classtype"];
            if (classtype == "record_struct") { classtype = "record struct"; }

            var files = new Dictionary<string, FileModel>();

            FileModel file;
            using (var sr = new StreamReader(req.Body))
                {
                    file = new JsonParserService().Parse(
                        sr,
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
                var output = (new CSharpGeneratorService()).GenerateDocument(
                    key,
                    innerFile.attributes,
                    innerFile.packages,
                    ns,
                    classtype);
                dto.files.Add(key, output);
            }

            log.LogInformation("C# HTTP trigger successfully processed a request.");

            return await Task.FromResult(new OkObjectResult(dto));
        }
    }
}


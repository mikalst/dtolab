using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using json2record.common;
using json2record.common.Services;
using json2record.func.DTOs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace json2record.func
{
    public static class ParseToCSharp
    {
        [Function("parse/csharp")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext,
            string name)
        {
            var log = executionContext.GetLogger("ParseToCSharp");

            name = name ?? "dto";

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

            var response = HttpResponseData.CreateResponse(req);
            response.WriteString(JsonSerializer.Serialize(dto));
            response.Headers.Add("Content-Type", new []{ "application/json" });
            
            log.LogInformation("C# HTTP trigger successfully processed a request.");

            return await Task.FromResult(response);
        }
    }
}


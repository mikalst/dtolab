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
    public static class ParseJSON
    {
        [FunctionName("parse")]
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

            var output = new OutputDTO() {
                name = name,
                files = files
            };

            log.LogInformation("C# HTTP trigger successfully processed a request.");

            var response = new OkObjectResult(output);
            

            return Task.FromResult<IActionResult>(new OkObjectResult(output));
        }
    }
}


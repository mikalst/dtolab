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
    public static class ParseJSON
    {
        [Function("parse")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext,
            string name)
        {
            var log = executionContext.GetLogger("ParseJSON");
            log.LogInformation("C# HTTP trigger function processed a request.");

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
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var output = new OutputDTO() {
                name = name,
                files = files
            };

            var response = HttpResponseData.CreateResponse(req);
            response.WriteString(JsonSerializer.Serialize(output));
            return await Task.FromResult(response);
        }
    }
}


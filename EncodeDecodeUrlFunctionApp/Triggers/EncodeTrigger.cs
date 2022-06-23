using System;
using System.Net;
using System.Threading.Tasks;
using EncodeDecodeUrlFunctionApp.HttpRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace EncodeDecodeUrlFunctionApp.Triggers
{
    public class EncodeTrigger
    {
        private readonly ILogger<EncodeTrigger> _logger;

        public EncodeTrigger(ILogger<EncodeTrigger> log)
        {
            _logger = log;
        }

        [FunctionName("EncodeURL")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "EncodeURL" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UrlJson), Description = "URL", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                // Parse body
                var input = await req.ReadAsStringAsync();

                var postEndpoint = JsonConvert.DeserializeObject<UrlJson>(input);

                _logger.LogInformation($"Entered with " +
                    $"{postEndpoint}");

                // TODO validate input here 

                // TODO encode here 

                var encodedUrl = "http:test.com";

                // Return a 200 OK to the client with additional information
                return new OkObjectResult($"Decoded URL is {encodedUrl}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureFunctionBindings
{
    public class FunctionHttpSendMsg
    {
        private readonly ILogger<FunctionHttpSendMsg> _logger;

        public FunctionHttpSendMsg(ILogger<FunctionHttpSendMsg> logger)
        {
            _logger = logger;
        }

        [Function("HttpSendMsg")]
        [ServiceBusOutput("TopicOrQueueName1", Connection = "ServiceBusConnection", EntityType = ServiceBusEntityType.Queue)]
        public async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation($"C# HTTP trigger function processed a request for {context.InvocationId}.");
            string input = req.Query["input"];

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"HTTP response: Message sent Test input:{input}");

            return $"{input} {context.InvocationId}.";
        }

    }
}

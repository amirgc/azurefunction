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
        public async Task<OutputType> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation($"C# HTTP trigger function processed a request for {context.InvocationId}.");

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("HTTP response: Message sent");

            return new OutputType()
            {
                OutputEvent = "MyMessage",
                HttpResponse = response
            };
        }

        public class OutputType
        {
            [ServiceBusOutput("TopicOrQueueName1", Connection = "ServiceBusConnection")]
            public string OutputEvent { get; set; }

            public HttpResponseData HttpResponse { get; set; }
        }
    }
}

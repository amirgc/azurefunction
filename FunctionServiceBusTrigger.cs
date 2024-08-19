using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionBindings
{
    public class FunctionServiceBusTrigger
    {
        private readonly ILogger<FunctionServiceBusTrigger> _logger;

        public FunctionServiceBusTrigger(ILogger<FunctionServiceBusTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunctionServiceBusTrigger))]
        [BlobOutput("az204conatiner/test2.txt", Connection = "StorageConnection")]
        public async Task<string> Run(
            [ServiceBusTrigger("TopicOrQueueName1", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            await messageActions.CompleteMessageAsync(message);

            return message.Body.ToString();                ;
        }
    }
}

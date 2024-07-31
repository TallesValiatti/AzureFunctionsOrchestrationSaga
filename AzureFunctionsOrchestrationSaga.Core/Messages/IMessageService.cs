using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Messages;

public interface IMessageService
{
    Task PublishAsync(string queueName, string message);
}
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Messages;

public interface IMessageService
{
    Task PublishAsync(IMessage message);
}
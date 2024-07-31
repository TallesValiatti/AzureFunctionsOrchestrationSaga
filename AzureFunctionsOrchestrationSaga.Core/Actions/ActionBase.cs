using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public abstract class ActionBase<T, TW> : IAction 
    where T : IMessage
    where TW : IMessage
{
    public abstract Task ExecuteAsync(T message);

    public abstract Task CompensateAsync(TW message);
}
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookCarAction : ActionBase<BookCarMessage, CancelBookCarMessage>
{
    public override Task ExecuteAsync(BookCarMessage message)
    {
        throw new NotImplementedException();
    }

    public override Task CompensateAsync(CancelBookCarMessage message)
    {
        throw new NotImplementedException();
    }
}
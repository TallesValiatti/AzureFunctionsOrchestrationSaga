using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookFlightAction : ActionBase<BookFlightMessage, CancelBookFlightMessage>
{
    public override Task ExecuteAsync(BookFlightMessage message)
    {
        throw new NotImplementedException();
    }

    public override Task CompensateAsync(CancelBookFlightMessage message)
    {
        throw new NotImplementedException();
    }
}
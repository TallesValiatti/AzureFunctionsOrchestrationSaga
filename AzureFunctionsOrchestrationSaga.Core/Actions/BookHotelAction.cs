using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookHotelAction : ActionBase<BookHotelMessage, CancelBookHotelMessage>
{
    public override Task ExecuteAsync(BookHotelMessage message)
    {
        throw new NotImplementedException();
    }

    public override Task CompensateAsync(CancelBookHotelMessage message)
    {
        throw new NotImplementedException();
    }
}
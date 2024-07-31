using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookHotelAction(IMessageService messageService): ActionBase<BookHotelMessage, CancelBookHotelMessage>
{
    protected override async Task ExecuteAsync(BookHotelMessage message)
    {
        // Perform action
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookHotelCompleted)));
    }

    protected override Task CompensateAsync(CancelBookHotelMessage message)
    {
        return Task.CompletedTask;
    }
}
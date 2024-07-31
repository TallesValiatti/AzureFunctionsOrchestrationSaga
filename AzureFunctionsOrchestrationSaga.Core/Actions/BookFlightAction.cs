using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookFlightAction(IMessageService messageService): ActionBase<BookFlightMessage, CancelBookFlightMessage>
{
    protected override async Task ExecuteAsync(BookFlightMessage message)
    {
        // Perform action
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookFlightCompleted)));
    }

    protected override Task CompensateAsync(CancelBookFlightMessage message)
    {
        return Task.CompletedTask;
    }
}
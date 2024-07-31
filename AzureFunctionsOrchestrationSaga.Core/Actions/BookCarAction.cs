using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookCarAction(IMessageService messageService) : ActionBase<BookCarMessage, CancelBookCarMessage>
{
    protected override async Task ExecuteAsync(BookCarMessage message)
    {
        // Perform action
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookCarCompleted)));
    }

    protected override Task CompensateAsync(CancelBookCarMessage message)
    {
        return Task.CompletedTask;
    }
}
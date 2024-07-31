using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookCarAction(ILogger<BookCarAction> loggerFactory, IMessageService messageService) : ActionBase<BookCarMessage, CancelBookCarMessage>
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

    protected override async Task CompensateAsync(CancelBookCarMessage message)
    {
        // Perform action
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookCarCompensated)));
    }
}
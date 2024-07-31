using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookHotelAction(ILogger<BookCarAction> logger, IMessageService messageService): ActionBase<BookHotelMessage, CancelBookHotelMessage>
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

    protected override async Task CompensateAsync(CancelBookHotelMessage message)
    {
        // Perform action
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookHotelCompensated)));
    }
}
using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookFlightAction(ILogger<BookCarAction> logger, IMessageService messageService): ActionBase<BookFlightMessage, CancelBookFlightMessage>
{
    protected override async Task ExecuteAsync(BookFlightMessage message)
    {
        // Perform action

        //Error
        if (true)
        {
            logger.LogWarning("Unable to process the flight booking");
            await messageService.PublishAsync(
                nameof(ReplyMessage),
                JsonSerializer.Serialize(new ReplyMessage(
                    message.BookingTravelId,
                    EventType.BookFlightCompensated)));
        }
        else
        {
            await messageService.PublishAsync(
                nameof(ReplyMessage),
                JsonSerializer.Serialize(new ReplyMessage(
                    message.BookingTravelId,
                    EventType.BookFlightCompleted)));
        }
    }

    protected override async Task CompensateAsync(CancelBookFlightMessage message)
    {
        // Perform action
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookFlightCompensated)));
    }
}
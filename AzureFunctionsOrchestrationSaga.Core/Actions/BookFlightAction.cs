using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Entities;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Core.Actions;

public class BookFlightAction(ILogger<BookCarAction> logger, IMessageService messageService): ActionBase<BookFlightMessage, CancelBookFlightMessage>
{
    protected override async Task ExecuteAsync(BookFlightMessage message)
    {
        // Fail to book the flight
        // Must be compensated
        
        await messageService.PublishAsync(
            nameof(ReplyMessage),
            JsonSerializer.Serialize(new ReplyMessage(
                message.BookingTravelId,
                EventType.BookFlightCompensated)));
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
using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Entities;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using AzureFunctionsOrchestrationSaga.Core.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class ReplyFunction(
    ILoggerFactory loggerFactory, 
    IMessageService messageService,
    IBookingTravelRepository repository)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ReplyFunction>();

    [Function("ReplyFunction")]
    public async Task Run([RabbitMQTrigger("ReplyMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<ReplyMessage>(item);

        var bookingTravel = await  repository.GetByIdAsync(message!.BookingTravelId);

        if (bookingTravel is null)
            return;
        
        switch (message.Type)
        {
            case EventType.BookCarCompleted:
                await HandleBookCarCompletedAsync(bookingTravel);
                break;
            
            case EventType.BookHotelCompleted:
                await HandleBookHotelCompletedAsync(bookingTravel);
                break;
            
            case EventType.BookFlightCompleted:
                await HandleBookFlightCompletedAsync(bookingTravel);
                break;
            
            case EventType.BookCarCompensated:
                await HandleBookCarCompensatedAsync(bookingTravel);
                break;
            
            case EventType.BookHotelCompensated:
                await HandleBookHotelCompensatedAsync(bookingTravel);
                break;
            
            case EventType.BookFlightCompensated:
                await HandleBookFlightCompensatedAsync(bookingTravel);
                break;
        }
    }
    
    private async Task HandleBookCarCompletedAsync(BookingTravel bookingTravel)
    {
        _logger.LogWarning("Entity Id {id} - book car completed", bookingTravel.Id);
        
        bookingTravel.AddEvent(EventType.BookCarCompleted);
        
        await messageService.PublishAsync(
            nameof(BookHotelMessage),
            JsonSerializer.Serialize(new BookHotelMessage(bookingTravel.Id)));
    }
    
    private async Task HandleBookHotelCompletedAsync(BookingTravel bookingTravel)
    {
        _logger.LogWarning("Entity Id {id} - book hotel completed", bookingTravel.Id);
        
        bookingTravel.AddEvent(EventType.BookHotelCompleted);
        
        await messageService.PublishAsync(
            nameof(BookFlightMessage),
            JsonSerializer.Serialize(new BookFlightMessage(bookingTravel.Id)));
    }
    
    private async Task HandleBookFlightCompletedAsync(BookingTravel bookingTravel)
    {
        _logger.LogWarning("Entity Id {id} - book flight completed", bookingTravel.Id);
        
        bookingTravel.AddEvent(EventType.BookFlightCompleted);
    }
    
    private async Task HandleBookCarCompensatedAsync(BookingTravel bookingTravel)
    {
        _logger.LogWarning("Entity Id {id} - book car compensated", bookingTravel.Id);
        
        bookingTravel.AddEvent(EventType.BookCarCompensated);
    }
    
    private async Task HandleBookHotelCompensatedAsync(BookingTravel bookingTravel)
    {
        _logger.LogWarning("Entity Id {id} - book hotel compensated", bookingTravel.Id);
        
        bookingTravel.AddEvent(EventType.BookHotelCompensated);
        
        await messageService.PublishAsync(
            nameof(CancelBookCarMessage),
            JsonSerializer.Serialize(new CancelBookCarMessage(bookingTravel.Id)));
    }
    
    private async Task HandleBookFlightCompensatedAsync(BookingTravel bookingTravel)
    {
        _logger.LogWarning("Entity Id {id} - book flight compensated", bookingTravel.Id);
        
        bookingTravel.AddEvent(EventType.BookFlightCompensated);
        
        await messageService.PublishAsync(
            nameof(CancelBookHotelMessage),
            JsonSerializer.Serialize(new CancelBookHotelMessage(bookingTravel.Id)));
    }

}
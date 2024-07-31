using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Entities;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using AzureFunctionsOrchestrationSaga.Core.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CreateBookingTravelFunction(
    ILoggerFactory loggerFactory, 
    IBookingTravelRepository repository,
    IMessageService messageService)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateBookingTravelFunction>();

    [Function("CreateBookingTravelFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function,
            "post",
            Route = "booking-travel")] HttpRequestData req, FunctionContext executionContext)
    {
        var bookingTravel = new BookingTravel
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(bookingTravel);
        
        _logger.LogInformation("Entity Id {id} - booking travel created", bookingTravel.Id);

        await messageService.PublishAsync(
            nameof(BookCarMessage),
            JsonSerializer.Serialize(new BookCarMessage(bookingTravel.Id)));
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
}
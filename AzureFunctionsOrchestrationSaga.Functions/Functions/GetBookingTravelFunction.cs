using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Entities;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using AzureFunctionsOrchestrationSaga.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class GetBookingTravelFunction(
    ILoggerFactory loggerFactory, 
    IBookingTravelRepository repository)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<GetBookingTravelFunction>();

    [Function("GetBookingTravelFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function,
            "get",
            Route = "booking-travel/{id:Guid}")] HttpRequestData req,
        Guid id,
        FunctionContext executionContext)
    {
        var bookingTravel = await  repository.GetByIdAsync(id);
        
        return new JsonResult(bookingTravel);
    }
}
using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CreateBookFlightFunction(
    ILoggerFactory loggerFactory, 
    [FromKeyedServices(nameof(BookFlightAction))] IAction action)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateBookHotelFunction>();

    [Function("CreateBookFlightFunction")]
    public async Task Run([RabbitMQTrigger("BookFlightMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<BookFlightMessage>(item);

        await action.ExecuteAsync(message);
        
        _logger.LogInformation("Car booked!");
    }
}
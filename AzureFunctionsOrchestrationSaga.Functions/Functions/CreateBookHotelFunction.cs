using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CreateBookHotelFunction(
    ILoggerFactory loggerFactory, 
    [FromKeyedServices(nameof(BookHotelAction))] IAction action)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateBookHotelFunction>();

    [Function("CreateBookHotelFunction")]
    public async Task Run([RabbitMQTrigger("BookHotelMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<BookHotelMessage>(item);

        await action.ExecuteAsync(message);
        
        _logger.LogInformation("Car booked!");
    }
}
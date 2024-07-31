using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CreateBookCarFunction(
    ILoggerFactory loggerFactory, 
    [FromKeyedServices(nameof(BookCarAction))] IAction action)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateBookCarFunction>();

    [Function("CreateBookCarFunction")]
    public async Task Run([RabbitMQTrigger("BookCarMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<BookCarMessage>(item);

        await action.ExecuteAsync(message);
    }
}
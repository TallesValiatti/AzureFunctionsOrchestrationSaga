using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CancelBookCarFunction([FromKeyedServices(nameof(BookCarAction))] IAction action)
{
    [Function("CancelBookFlightFunction")]
    public async Task Run([RabbitMQTrigger("CancelBookCarMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<CancelBookCarMessage>(item);

        await action.CompensateAsync(message);
    }
}
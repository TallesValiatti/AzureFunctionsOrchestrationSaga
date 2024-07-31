using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CompensateBookFlightFunction([FromKeyedServices(nameof(BookFlightAction))] IAction action)
{
    [Function("CompensateBookFlightFunction")]
    public async Task Run([RabbitMQTrigger("CancelBookFlightMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<CancelBookFlightMessage>(item);

        await action.CompensateAsync(message);
    }
}
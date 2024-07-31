using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsOrchestrationSaga.Functions.Functions;

public class CancelBookHotelFunction([FromKeyedServices(nameof(BookHotelAction))] IAction action)
{
    [Function("CancelBookHotelFunction")]
    public async Task Run([RabbitMQTrigger("CancelBookHotelMessage", ConnectionStringSetting = "RabbitMqConnectionString")] string item, FunctionContext context)
    {
        var message = System.Text.Json.JsonSerializer.Deserialize<CancelBookHotelMessage>(item);

        await action.CompensateAsync(message);
    }
}
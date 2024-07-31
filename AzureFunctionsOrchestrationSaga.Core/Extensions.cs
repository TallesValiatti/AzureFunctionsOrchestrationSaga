using AzureFunctionsOrchestrationSaga.Core.Actions;
using AzureFunctionsOrchestrationSaga.Core.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionsOrchestrationSaga.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, string rabbitMqConnectionString)
    {
        services.AddMessageService(rabbitMqConnectionString);
        services.AddActions();
        return services;
    }
    
    private static IServiceCollection AddMessageService(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMessageService>(x => new RabbitMqService(connectionString));
        return services;
    }
    
    private static IServiceCollection AddActions(this IServiceCollection services)
    {
        // services.AddKeyedSingleton<IAction, BookFlightAction>(nameof(BookFlightAction));
        // services.AddKeyedSingleton<IAction, BookHotelAction>(nameof(BookHotelAction));
        services.AddKeyedSingleton<IAction, BookCarAction>(nameof(BookCarAction));
        
        return services;
    }
}
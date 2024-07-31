using AzureFunctionsOrchestrationSaga.Core;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((host, services) =>
    {
        services.AddCore(host.Configuration["RabbitMqConnectionString"]!);
    })
    .Build();

host.Run();
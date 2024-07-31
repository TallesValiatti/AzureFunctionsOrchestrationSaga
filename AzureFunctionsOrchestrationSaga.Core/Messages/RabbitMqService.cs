using System.Text;
using System.Text.Json;
using AzureFunctionsOrchestrationSaga.Core.Messages.Models;
using RabbitMQ.Client;

namespace AzureFunctionsOrchestrationSaga.Core.Messages;

public class RabbitMqService(string connectionSting) : IMessageService
{
    private IConnection CreateConnection()
    {
        ConnectionFactory connection = new ConnectionFactory
        {
            Uri = new Uri(connectionSting),
            DispatchConsumersAsync = true
        };

        var channel = connection.CreateConnection();
        return channel;
    }
    
    public async Task PublishAsync(string queueName, string message)
    {
        await Task.Yield();
        
        using var connection = CreateConnection();
        using var chanel = connection.CreateModel();

        var body = Encoding.UTF8.GetBytes(message);
  
        chanel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        
        chanel.BasicPublish(exchange: string.Empty, 
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
}
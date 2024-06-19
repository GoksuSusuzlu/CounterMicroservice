using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Application.Services.Counters;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Services.RabbitMQResponderService;
public class RabbitMQResponderService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ICounterService _counterService;

    public RabbitMQResponderService(ICounterService counterService)
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "counterQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        _counterService = counterService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Guid counterId = Guid.Parse(message);
            var counterDetails = _counterService.GetAsync(predicate: c => c.Id == counterId);

            var responseBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(counterDetails));
            var replyProps = _channel.CreateBasicProperties();
            replyProps.CorrelationId = ea.BasicProperties.CorrelationId;

            _channel.BasicPublish(
                exchange: "",
                routingKey: ea.BasicProperties.ReplyTo,
                basicProperties: replyProps,
                body: responseBytes);
        };

        _channel.BasicConsume(queue: "counterQueue", autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}

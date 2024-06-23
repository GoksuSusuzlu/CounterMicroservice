using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Counters;
using Microsoft.Extensions.Configuration;

namespace Application.Services.RabbitMQResponderService
{
    public class RabbitMQResponderService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQResponderService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:HostName"],
                Port = int.Parse(configuration["RabbitMQ:Port"]),
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "counterQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Guid counterId = Guid.Parse(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var counterService = scope.ServiceProvider.GetRequiredService<ICounterService>();
                    var counterDetails = await counterService.GetAsync(c => c.Id == counterId);
                    counterDetails.LatestIndexInfo = "rabbit mq test";

                    var responseBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(counterDetails));
                    var replyProps = _channel.CreateBasicProperties();
                    replyProps.CorrelationId = ea.BasicProperties.CorrelationId;

                    _channel.BasicPublish(
                        exchange: "",
                        routingKey: ea.BasicProperties.ReplyTo,
                        basicProperties: replyProps,
                        body: responseBytes);

                    Console.WriteLine(" [x] Sent {0}", message);  // Add logging for debugging
                }
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
}

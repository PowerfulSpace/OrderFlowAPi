using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PS.OrderProcessingService.Application.Interfaces;
using PS.OrderProcessingService.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PS.OrderProcessingService.Infrastructure.Messaging
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMqConsumer> _logger;
        private readonly IOrderProcessor _orderProcessor;
        private IConnection? _connection;
        private IModel? _channel;

        public RabbitMqConsumer(IConfiguration configuration, ILogger<RabbitMqConsumer> logger, IOrderProcessor orderProcessor)
        {
            _configuration = configuration;
            _logger = logger;
            _orderProcessor = orderProcessor;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "order_queue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            _logger.LogInformation("RabbitMQ Consumer started.");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var order = JsonSerializer.Deserialize<Order>(message);
                    if (order != null)
                    {
                        _logger.LogInformation($"Received Order: {order.Id}");
                        await _orderProcessor.ProcessOrderAsync(order);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process order message.");
                }
            };

            _channel.BasicConsume(queue: "order_queue",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitMQDemo
{
    public class Testcls : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
    public class OrderCreatedConsumer : BackgroundService
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _queueName;
        private IChannel _channel;

        public OrderCreatedConsumer(
            IRabbitMQConnection connection,
            IConfiguration config,
            IServiceProvider serviceProvider)
        {
            _connection = connection;
            _config = config;
            _serviceProvider = serviceProvider;
            _queueName = _config["RabbitMQ:QueueName"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel = await _connection.CreateChannel();

            // 声明交换机和队列
            await _channel.ExchangeDeclareAsync(
                  exchange: _config["RabbitMQ:ExchangeName"],
                  type: ExchangeType.Direct,
                  durable: true);

            await _channel.QueueDeclareAsync(
                   queue: _queueName,
                   durable: true,
                   exclusive: false,
                   autoDelete: false);

            await _channel.QueueBindAsync(
                  queue: _queueName,
                  exchange: _config["RabbitMQ:ExchangeName"],
                  routingKey: "order.created");

            // 设置QoS
            await _channel.BasicQosAsync(0, 1, false);

            var consumer = new AsyncEventingBasicConsumer(_channel); //EventingBasicConsumer
            //Received
            consumer.ReceivedAsync += async (model, ea) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var processor = scope.ServiceProvider.GetRequiredService<IOrderProcessor>();

                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                    await processor.ProcessOrderAsync(orderEvent);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false); //BasicAck
                }
                catch (Exception ex)
                {
                    // 记录错误日志
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false); //BasicNack
                }
            };

            await _channel.BasicConsumeAsync(
                 queue: _queueName,
                 autoAck: false,
                 consumer: consumer);        
        }

        public override async void Dispose()
        {
           await _channel?.CloseAsync();//.Close();
            base.Dispose();
        }
    }
}

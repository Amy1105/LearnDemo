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

        /// <summary>
        /// 接收消息最推荐且最便捷的方式是使用接口设置订阅IAsyncBasicConsumer。
        /// 这样，消息到达时就会自动投递，无需主动请求。
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
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

            //EventingBasicConsumer:实现消费者的一种方法是使用便利类 AsyncEventingBasicConsumer，
            //它将交付和其他消费者生命周期事件作为 C# 事件分派
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

                    //具体消费动作
                    await processor.ProcessOrderAsync(orderEvent);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false); //BasicAck
                }
                catch (Exception ex)
                {
                    // 记录错误日志
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false); //BasicNack
                }
            };

            //BasicConsume 方法是消费者（Consumer）开始消费队列消息的核心方法。
            //它的作用是将当前信道（Channel）设置为监听指定队列，并在消息到达时自动触发回调处理
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

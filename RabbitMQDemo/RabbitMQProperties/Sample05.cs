using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQDemo.RabbitMQProperties
{
    /// <summary>
    /// 主题模式
    /// </summary>
    public class Sample07Producer
    {
        public static async void Run()
        {
            var factory = new ConnectionFactory
            {
                HostName = "192.168.2.2",
                Port = 5672,
                UserName = "admin",
                Password = "admin",
                VirtualHost = "my_vhost"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            const string exchangeName = "sample.recommendation.topic";
            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic);

            var props = new BasicProperties
            {
                Persistent = true
            };

            while (true)
            {
                Console.WriteLine("请输入要发送的推荐信息：");
                var message = Console.ReadLine();

                Console.WriteLine("请输入要发布的主题，以逗号分隔：");
                var topicsInput = Console.ReadLine();
                var topics = topicsInput.Split(',');

                // 发布推荐信息到多个主题
                foreach (var topic in topics)
                {
                    ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(message).AsMemory();
                    await channel.BasicPublishAsync(exchange: exchangeName, routingKey: topic.Trim(), mandatory: true, basicProperties: props, body: body);

                    // 参数不一致，也能行？？？？
                    // await channel.BasicPublishAsync(exchange: string.Empty, routingKey: topic.Trim(), body: body, basicProperties: props, mandatory: true);

                    Console.WriteLine($"推荐信息已发布到主题：[{topic.Trim()}]");
                }
            }
        }
    }

    /// <summary>
    /// 主题模式
    /// </summary>
    public static class Sample05Consumer
    {
        public static async void Run()
        {
            var factory = new ConnectionFactory
            {
                HostName = "192.168.2.2",
                Port = 5672,
                UserName = "admin",
                Password = "admin",
                VirtualHost = "my_vhost"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            const string exchangeName = "sample.recommendation.topic";
            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic);

            Console.WriteLine("请输入要订阅的主题，以逗号分隔：");
            var topicsInput = Console.ReadLine();
            var topics = topicsInput.Split(',');

            var queueName = await channel.QueueDeclareAsync();

            // 订阅多个主题的推荐信息
            foreach (var topic in topics)
            {
                await channel.QueueBindAsync(queueName.QueueName, exchangeName, topic.Trim());
            }

            Console.WriteLine("等待推荐信息...");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {

                try
                {
                    var routingKey = ea.RoutingKey;

                    // 获取消息体（ReadOnlyMemory<byte>）
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.Span);
                    Console.WriteLine($"Received: {message}");

                    // 模拟异步操作（如数据库写入、HTTP请求等）
                    await Task.Delay(1000);

                    // 手动确认消息（ack）
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                    Console.WriteLine($"收到来自主题：[{routingKey}] 的推荐信息：[{message}]");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    // 可根据需要拒绝消息（nack或reject）
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await channel.BasicConsumeAsync(queueName, true, consumer);

            Console.ReadLine();
        }
    }
}

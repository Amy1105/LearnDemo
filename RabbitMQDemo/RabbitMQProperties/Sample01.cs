using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQDemo.RabbitMQProperties
{
    /// <summary>
    /// 工作队列模式初步实现
    /// </summary>
    public class Sample01Producer
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
            var props = new BasicProperties
            {
                Persistent = true
            };
            await channel.QueueDeclareAsync("sms.queue", false, false, false);

            for (var i = 0; i < 100; i++)
            {
                var message = $"phone:13900000{i},message:您的车票已预定成功";
                var body = Encoding.UTF8.GetBytes(message);
                await channel.BasicPublishAsync("", "sms.queue", true, props, body);
                Console.WriteLine($"消息已发送：{message} ");
            }
        }
    }
    /// <summary>
    /// 工作队列模式初步实现
    /// </summary>
    public class Sample01Consumer
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
            await channel.QueueDeclareAsync("sms.queue", false, false, false);
            var consumer = new AsyncEventingBasicConsumer(channel);
            var count = 0;
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                // 模拟发短信的操作延迟
                await Task.Delay(800);
                Console.WriteLine($"短信已发送{++count}条：{message}");
            };
            await channel.BasicConsumeAsync(queue: "sms.queue", true, consumer);
            Console.ReadLine();
        }
    }
}

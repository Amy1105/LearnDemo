using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQDemo.Models;
using System.Text;

namespace RabbitMQDemo.RabbitMQProperties
{
    /// <summary>
    /// 发布订阅模式
    /// </summary>
    public class Sample05Producer
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

            using var connection =await factory.CreateConnectionAsync();
            using var channel =await connection.CreateChannelAsync();

            // 声明交换机
            const string exchangeName = "sample.msg.fanout";
           await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout);

            var props = new BasicProperties
            {
                Persistent = true
            };

            for (var i = 0; i < 100; i++)
            {
                var message = $"publisher:张三,message:hello {i}";              
                ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(message).AsMemory();
                Console.WriteLine($"[{message}] 已发送");
                await channel.BasicPublishAsync(exchange: exchangeName, routingKey: "", mandatory: true, basicProperties: props, body: body);
            }
        }
    }

    /// <summary>
    /// 发布订阅模式
    /// </summary>
    public static class Sample03Consumer
    {
        public static async void Run(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "192.168.2.2",
                Port = 5672,
                UserName = "admin",
                Password = "admin",
                VirtualHost = "my_vhost"
            };

            using var connection =await factory.CreateConnectionAsync();
            using var channel =await connection.CreateChannelAsync();

            // 声明交换机
            const string exchangeName = "sample.msg.fanout";
            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout);

            var queneuName = args[0];
          await  channel.QueueDeclareAsync(queneuName, false, false, false);

            // 绑定队列
         await   channel.QueueBindAsync(queneuName, exchangeName, "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync +=async (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[{message}]已接收");
               await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            };
           await  channel.BasicConsumeAsync(queneuName, false, consumer);
            Console.ReadLine();
        }
    }
}

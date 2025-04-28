using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQDemo.RabbitMQProperties
{
    /// <summary>
    /// RabbitMQ 事务机制
    /// </summary>
    public class Sample02Producer
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
            var props = new BasicProperties
            {
                Persistent = true
            };

            for (var i = 0; i < 100; i++)
            {
                var message = $"phone:13900000{i},message:您的车票已预定成功";

                await channel.TxSelectAsync();
                try
                {
                    // 发送消息               
                    ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(message).AsMemory();
                    await channel.BasicPublishAsync(exchange: "", routingKey: "sms.queue", mandatory: true, basicProperties: props, body: body);
                    // 提交事务
                    await channel.TxCommitAsync();

                    Console.WriteLine($"消息已发送：{message} ");
                }
                catch (Exception e)
                {
                    // 发送消息失败，回滚事务
                    await channel.TxRollbackAsync();
                    Console.WriteLine("发送消息失败");
                }
            }
        }
    }

    /// <summary>
    /// RabbitMQ 确认机制
    /// </summary>
    public class Sample03Producer
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
            await  channel.QueueDeclareAsync("sms.queue", false, false, false);
            channel.ConfirmSelect();

            for (var i = 0; i < 100; i++)
            {
                var message = $"phone:13900000{i},message:您的车票已预定成功";
                var body = Encoding.UTF8.GetBytes(message);


                try
                {
                    // 发送消息
                 await   channel.BasicPublishAsync("", "sms_queue", true, props, body);
                    // 等待确认
                    //WaitForConfirmsOrDie   WaitForConfirmsAsync
                     channel.WaitForConfirmsOrDie(TimeSpan.FromMilliseconds(100));

                    Console.WriteLine($"消息已发送：{message} ");
                }
                catch (Exception e)
                {
                    // 发送消息失败，处理重发逻辑
                    Console.WriteLine("发送消息失败");
                }
            }
        }
    }

    /// <summary>
    /// RabbitMQ 发布确认模式
    /// </summary>
    public class Sample04Producer
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
            channel.ConfirmSelect();
            var props = new BasicProperties
            {
                Persistent = true
            };
            for (var i = 0; i < 100; i++)
            {
                var message = $"phone:13900000{i},message:您的车票已预定成功";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicAckAsync +=async (sender, args) =>
                {
                    await Thread.Sleep(1000);
                    // 消息确认
                    Console.WriteLine($"消息已发送：{message} {args.DeliveryTag}");
                };
                channel.BasicNackAsync +=async (sender, args) =>
                {
                    await Thread.Sleep(1000);
                    // 消息未确认，处理重发逻辑
                    Console.WriteLine("发送消息失败");
                };

              await  channel.BasicPublishAsync("", "sms_queue", true, props, body);
            }
        }
    }

    /// <summary>
    /// 消费确认
    /// </summary>
    public static class Sample02Consumer
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

          await  channel.QueueDeclareAsync("sms.queue", false, false, false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            var count = 0;
            consumer.ReceivedAsync +=async (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                // 模拟发短信的操作延迟
                Thread.Sleep(800);
                Console.WriteLine($"短信已发送{++count}条：{message}");
               await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            };
          await  channel.BasicConsumeAsync(queue: "sms.queue", false, consumer);
            Console.ReadLine();
        }
    }
}

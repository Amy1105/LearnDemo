using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQDemo.RabbitMQProperties
{

    /// <summary>
    /// 路由模式
    /// </summary>
    public class Sample06Producer
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
            const string exchangeName = "sample.order.direct";
            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

            // 创建供应商队列并绑定到交换机
            CreateSupplierQueue(channel, "supplier1", exchangeName);
            CreateSupplierQueue(channel, "supplier2", exchangeName);

            // 模拟订单发送
            SendOrder(channel, exchangeName, "supplier1", "Order 1");
            SendOrder(channel, exchangeName, "supplier2", "Order 2");
        }

        static async void CreateSupplierQueue(IChannel channel, string supplierId, string exchangeName)
        {
            // 声明队列
            var queueName = "supplier.queue." + supplierId;
           await channel.QueueDeclareAsync(queueName, false, false, false, null);

            // 绑定队列到交换机
           await channel.QueueBindAsync(queueName, exchangeName, supplierId);
        }

        static async void SendOrder(IChannel channel, string exchangeName, string routingKey, string order)
        {
            var props = new BasicProperties
            {
                Persistent = true
            };
            ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(order).AsMemory();
            await channel.BasicPublishAsync(exchange: exchangeName, routingKey: routingKey, mandatory: true, basicProperties: props, body: body);
            Console.WriteLine($"发送订单 [{order}] 到供应商[{routingKey}]");
        }
    }


    public static class Sample04Consumer
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
            const string exchangeName = "sample.order.direct";
            await  channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

            // 创建供应商队列并绑定到交换机
            CreateSupplierQueue(channel, "supplier1", exchangeName);
            CreateSupplierQueue(channel, "supplier2", exchangeName);

            // 注册消费者
            RegisterConsumer(channel, "supplier1");
            RegisterConsumer(channel, "supplier2");

            Console.ReadLine();
        }

        static async void CreateSupplierQueue(IChannel channel, string supplierId, string exchangeName)
        {
            // 声明队列
            var queueName = "supplier.queue." + supplierId;
            await  channel.QueueDeclareAsync(queueName, false, false, false, null);

            // 绑定队列到交换机
            await channel.QueueBindAsync(queueName, exchangeName, supplierId);
        }

        static async void RegisterConsumer(IChannel channel, string supplierId)
        {
            var queueName = "supplier.queue." + supplierId;

            // 创建消费者
            var consumer = new AsyncEventingBasicConsumer(channel);

            // 注册消息处理函数
            consumer.ReceivedAsync +=async (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"接受订单[{message}] 来自供应商[{supplierId}]");
               await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            };
           await channel.BasicConsumeAsync(queueName, false, consumer);
        }
    }     
}

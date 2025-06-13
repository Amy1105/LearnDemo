using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using VOL.Core.RabbitMQManager;

namespace RabbitMQDemo.Services
{
    /// <summary>
    /// 生产者
    /// </summary>
    public interface IMessageProducer
    {
        void SendMessage<T>(List<T> message);
    }

    /// <summary>
    /// 定义成作用域生命周期，每次请求只有一个connection对象
    /// </summary>
    public class BopProducer : IMessageProducer
    {
        private IRabbitMQConnection _connection;
        private readonly string routingKey;
        public BopProducer(IRabbitMQConnection connection, IOptions<RabbitMQOptions> options)
        {
            _connection = connection;
            routingKey = options.Value.RouteKey;
        }

        /// <summary>
        /// 待优化，应用程序在使用 RabbitMQ 之前，必须先连接到 某个 RabbitMQ 节点。该连接将用于执行所有后续操作。
        /// 连接应该是长期有效的。如果每次操作（例如发布消息）都打开一个连接，效率会非常低，因此 强烈建议不要这样做。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messages"></param>       
        public async void SendMessage<T>(List<T> messages)
        {
            var connection =await _connection.GetConnection();
            try
            {
                using (var channel = await connection.CreateChannelAsync())
                {
                    // 使用信道发送消息
                    // 声明交换机          
                    await channel.ExchangeDeclareAsync(RabbitMQConfig.exchangeName, ExchangeType.Direct);
                    // 声明队列
                    var queueName = RabbitMQConfig.GetQueueName(routingKey);

                    // 声明队列作为延迟队列   30mins,绑定死信队列         
                  await  channel.QueueDeclareAsync(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: RabbitMQConfig.QueueArguments);

                    // 绑定队列到交换机
                   await channel.QueueBindAsync(queueName, RabbitMQConfig.exchangeName, routingKey);
                  
                    channel.BasicAcksAsync+=async (sender, args) =>
                    {
                        await Task.Delay(100);
                        // 消息确认
                        Console.WriteLine($"消息已发送：{args.DeliveryTag}");
                        //修改bop的推送标志，错误消息置空
                    };
                    channel.BasicNacksAsync += async (sender, args) =>
                    {
                        await Task.Delay(100);
                        // 消息未确认，处理重发逻辑
                        Console.WriteLine("发送消息失败");
                        //修改bop的推送标志，并记录推送失败的错误信息
                    };
                    channel.BasicReturnAsync +=async (sender, args) =>
                    {
                        await Task.Delay(100);
                        // 消息未正确路由
                        Console.WriteLine("发送消息失败");
                    };

                    foreach (var message in messages)
                    {                       
                        var props = RabbitMQConfig.CreateDefaultProperties(channel, routingKey);
                        // 发送消息到交换机

                        var body = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
                   
                        await channel.BasicPublishAsync(exchange:RabbitMQConfig.exchangeName,
                            routingKey: routingKey, body: body,
                           mandatory: true, 
                         basicProperties: props);
                        Console.WriteLine($"成功推送1条bop消息");
                    }
                }
            }
            finally
            {
                _connection.ReturnConnection(connection);
            }
        }
    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace VOL.Core.RabbitMQManager
{
    /// <summary>
    /// 生产者
    /// </summary>
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }

    /// <summary>
    /// 定义成作用域生命周期，每次请求只有一个connection对象
    /// </summary>
    public class BopProducer : IMessageProducer
    {
        private readonly IRabbitMQConnection _connection;
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
        /// <param name="message"></param>       
        public async void SendMessage<T>(T message)
        {         
            // 与连接一样，通道也应具有长寿命。为每个操作都打开一个新通道效率极低，因此强烈建议不要这样做。
            // 然而，通道的生命周期可能比连接更短。例如，某些协议错误会自动关闭通道。
            // 如果应用程序能够从中恢复，则可以打开一个新通道并重试该操作。
            using var channel = _connection.CreateChannel();

            // 声明交换机          
            channel.ExchangeDeclare(RabbitMQConfig.exchangeName, ExchangeType.Direct);
            // 声明队列
            var queueName = RabbitMQConfig.GetQueueName(routingKey);
            // 绑定队列到交换机
            channel.QueueBind(queueName, RabbitMQConfig.exchangeName, routingKey);

            // 声明队列作为延迟队列   30mins,绑定死信队列         
            channel.QueueDeclare(queue:queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: RabbitMQConfig.QueueArguments);

            channel.ConfirmSelect();

            channel.BasicAcks += (sender, args) =>
            {
                // 消息确认
                Console.WriteLine($"消息已发送：{message} {args.DeliveryTag}");
                //修改bop的推送标志，错误消息置空
            };
            channel.BasicNacks += (sender, args) =>
            {
                // 消息未确认，处理重发逻辑
                Console.WriteLine("发送消息失败");
                //修改bop的推送标志，并记录推送失败的错误信息
            };
            channel.BasicReturn += (sender, args) =>
            {
                // 消息未正确路由
                Console.WriteLine("发送消息失败");                
            };

            //发送消息
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var props = RabbitMQConfig.CreateDefaultProperties(channel,routingKey);
          
            // 发送消息到交换机
            channel.BasicPublish(exchangeName, routingKey, props, body);          
        }
    }
}

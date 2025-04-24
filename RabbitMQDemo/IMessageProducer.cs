using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace RabbitMQDemo
{
    /// <summary>
    /// 生产者
    /// </summary>
    public interface IMessageProducer
    {
        void SendMessage<T>(T message, string routingKey);
    }

    public class RabbitMQProducer : IMessageProducer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly string _exchangeName;

        public RabbitMQProducer(IRabbitMQConnection connection, IConfiguration config)
        {
            _connection = connection;
            _exchangeName = config["RabbitMQ:ExchangeName"];
        }

        /// <summary>
        /// 待优化，应用程序在使用 RabbitMQ 之前，必须先连接到 某个 RabbitMQ 节点。该连接将用于执行所有后续操作。
        /// 连接应该是长期有效的。如果每次操作（例如发布消息）都打开一个连接，效率会非常低，因此 强烈建议不要这样做。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="routingKey"></param>
        public async void SendMessage<T>(T message, string routingKey)
        {
            // 与连接一样，通道也应具有长寿命。为每个操作都打开一个新通道效率极低，因此强烈建议不要这样做。
            // 然而，通道的生命周期可能比连接更短。例如，某些协议错误会自动关闭通道。
            // 如果应用程序能够从中恢复，则可以打开一个新通道并重试该操作。
            using var channel =await _connection.CreateChannel();



            //  声明一个交换机和一个队列，然后将它们绑定在一起
            await channel.ExchangeDeclareAsync(
                exchange: _exchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = new BasicProperties
            {
                Persistent = true  // 消息持久化
            };

            //mandatory：true 强制性的
            //更多的需要参见BasicProperties属性

            await channel.BasicPublishAsync(exchange: _exchangeName, 
                routingKey: routingKey, 
                body: body, 
                basicProperties: properties,
                mandatory: true);
        
        }
    }
}

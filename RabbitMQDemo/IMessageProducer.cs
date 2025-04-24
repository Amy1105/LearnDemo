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

        public async void SendMessage<T>(T message, string routingKey)
        {
            using var channel =await _connection.CreateChannel();

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

            await channel.BasicPublishAsync(exchange: _exchangeName, 
                routingKey: routingKey, 
                body: body, 
                basicProperties: properties,
                mandatory: true);
        
        }
    }
}

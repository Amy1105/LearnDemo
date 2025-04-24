using RabbitMQ.Client;
using System.Text;

namespace RabbitMQDemo
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        Task<IChannel> CreateChannel();


    }

    public class RabbitMQConnection : IRabbitMQConnection
    {     
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public async Task<IChannel> CreateChannel()
        {
            if (!IsConnected)
            {
                _connection = await _connectionFactory.CreateConnectionAsync();
            }
            return await _connection.CreateChannelAsync();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _disposed = true;
        }       
    }
}

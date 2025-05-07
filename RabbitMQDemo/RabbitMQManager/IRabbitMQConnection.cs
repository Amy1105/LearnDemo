using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;

namespace VOL.Core.RabbitMQManager
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        IModel CreateChannel();
    }

    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;
        private readonly ILogger<RabbitMQConnection> _logger;

        public RabbitMQConnection(IOptions<RabbitMQOptions> options, ILogger<RabbitMQConnection> logger)
        {

            _logger = logger;
           
            var factory = new ConnectionFactory
            {
                HostName = options.Value.HostName,
                Port = options.Value.Port,
                VirtualHost = options.Value.VirtualHost,
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                AutomaticRecoveryEnabled = true, // 自动重连
                 NetworkRecoveryInterval = TimeSpan.FromSeconds(10) // 重试间隔
            };
            _connection = factory.CreateConnection();
            _logger.LogInformation("RabbitMQ 连接已建立");            
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public  IModel CreateChannel()
        {
            if (!IsConnected)
            {
                _connection = _connectionFactory.CreateConnection();
            }
            return  _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _disposed = true;
        }
    }
}

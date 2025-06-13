using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Collections.Concurrent;

namespace VOL.Core.RabbitMQManager
{
    public interface IRabbitMQConnection : IDisposable
    {       
       Task<IConnection> GetConnection();

        void ReturnConnection(IConnection connection);
    }

    public class RabbitMQConnectionPool : IRabbitMQConnection ,IDisposable
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1); // 初始计数为1的信号量
        private readonly  IConnectionFactory _connectionFactory;
        private readonly int _maxPoolSize=10;      
        private readonly ILogger<RabbitMQConnectionPool> _logger;
        private static readonly object _lock = new object();
        private readonly ConcurrentStack<IConnection> _connections = new ConcurrentStack<IConnection>();
        private bool _disposed = false; // 标记是否已释放资源
      
        public RabbitMQConnectionPool(IOptions<RabbitMQOptions> options, ILogger<RabbitMQConnectionPool> logger)
        {

            _logger = logger;
            _connectionFactory = new ConnectionFactory
            {
                HostName = options.Value.HostName,
                Port = options.Value.Port,
                VirtualHost = options.Value.VirtualHost,
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                AutomaticRecoveryEnabled = true, // 自动重连
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10) // 重试间隔
            };
        }
       
        public async Task<IConnection> GetConnection()
        {           
            // 检查是否已释放，防止在对象已销毁后继续使用
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(RabbitMQConnectionPool));
            }
            await _semaphore.WaitAsync(); // 异步等待获取锁
            try
            {
                if (_connections.TryPop(out var connection))
                {
                    return connection;
                }
                // rabbitmqClient 7.1.2
                return await _connectionFactory.CreateConnectionAsync();
            }
            finally
            {
                _semaphore.Release(); // 释放锁
            }
            // rabbitmqClient 6.8.1
            //lock (_connections)
            //{
            //    if (_connections.Count > 0)
            //    {
            //        return _connections.Pop();
            //    }
            //    return  _connectionFactory.CreateConnection();
            //}
        }

        public void ReturnConnection(IConnection connection)
        {
            // 检查是否已释放
            if (_disposed)
            {
                connection.Dispose(); // 直接释放传入的连接
                return;
            }

            lock (_connections)
            {
                if (_connections.Count < _maxPoolSize && connection.IsOpen)
                {
                    _connections.Push(connection);
                }
                else
                {
                    connection.Dispose();
                }
            }
        }

        // 实现 IDisposable 接口
        public void Dispose()
        {
            // 允许多次调用 Dispose，但只执行一次释放逻辑
            Dispose(true);
            GC.SuppressFinalize(this); // 阻止 GC 调用终结器
        }

        // 受保护的虚方法，允许子类重写释放逻辑
        protected virtual  async void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _semaphore.WaitAsync(); // 异步等待获取锁
                    // 释放托管资源（如关闭所有连接）
                    try
                    {
                        while (_connections.TryPop(out var connection))
                        {                           
                            try
                            {
                                if (connection.IsOpen)
                                {
                                    await connection.CloseAsync();
                                }
                                connection.Dispose();
                            }
                            catch (Exception ex)
                            {
                                // 记录日志或处理异常
                                Console.WriteLine("rabbitMQ Connection Dispose异常："+ex.Message);
                            }
                        }                                         
                    }
                    finally
                    {
                        _semaphore.Release(); // 释放锁
                    }                                      
                }
                // 释放非托管资源（如果有）
                // 示例：关闭文件句柄、数据库连接等
                _disposed = true;
            }
        }

        // 终结器（析构函数）：仅在忘记调用 Dispose 时由 GC 调用
        ~RabbitMQConnectionPool()
        {
            Dispose(false); // 仅释放非托管资源
        }
    }
}

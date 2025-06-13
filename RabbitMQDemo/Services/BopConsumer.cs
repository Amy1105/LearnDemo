using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Claims;
using System.Text;
using VOL.Core.RabbitMQManager;

namespace RabbitMQDemo.Services
{
    /// <summary>
    /// 后台线程-消费bop数据
    /// </summary>
    public class BopConsumerService : BackgroundService
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BopConsumerService> _logger;
        private readonly IOptions<RabbitMQOptions> _options;
        private IConnection connection = null;
        public BopConsumerService(
            IRabbitMQConnection connection, 
            IOptions<RabbitMQOptions> options, ILogger<BopConsumerService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _connection = connection;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("BopConsumer-服务启动");
            _logger.LogInformation("服务启动");
            string _routeKey = _options.Value.RouteKey;
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    // 1. 建立持久化连接和通道
                    connection ??= await _connection.GetConnection();
                    using var channel = await connection.CreateChannelAsync();

                    // 2. 声明交换机和队列（一次性）
                    await channel.ExchangeDeclareAsync(RabbitMQConfig.exchangeName, ExchangeType.Direct);
                    var queueName = RabbitMQConfig.GetQueueName(_routeKey);
                    await channel.QueueDeclareAsync(
                        queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: RabbitMQConfig.QueueArguments);
                    await channel.QueueBindAsync(queueName, RabbitMQConfig.exchangeName, _routeKey);
                    await channel.BasicQosAsync(prefetchCount: 1, prefetchSize: 0, global: false);

                    // 3. 创建消费者
                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (model, eventArgs) =>
                    {
                        var cancellationToken = stoppingToken;
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        try
                        {
                            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                            _logger.LogInformation($"收到消息: {message}");

                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var processService = scope.ServiceProvider.GetRequiredService<IPorcessService>();
                                // 模拟HttpContext
                                var mockHttpContext = new DefaultHttpContext();
                                mockHttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(
                                    new[] { new Claim(ClaimTypes.NameIdentifier, "administrators") }
                                ));
                              //VOL.Core.Utilities.HttpContext.Configure(new MockHttpContextAccessor(mockHttpContext));
                                // 业务处理...
                                var result = await processService.PopBopMessage(message);

                                if (!string.IsNullOrEmpty(result))
                                {
                                    // 处理失败，重新入队,不用考虑失败重试机制，因为rabbitmq消息确认采用手动确认机制，如果该消息没有成功返回给mq，
                                    // 那这条消息回一直在队列中，等待下次消费，并返回mq确认结果
                                    if (channel.IsOpen && !cancellationToken.IsCancellationRequested) //避免channel断开引发的异常，或web程序关闭的取消事件
                                    {
                                        await channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: true);
                                        _logger.LogWarning($"消息已重新入队: {Encoding.UTF8.GetString(eventArgs.Body.ToArray())}");
                                    }
                                }
                                else
                                {
                                    // 确认消息
                                    if (channel.IsOpen && !cancellationToken.IsCancellationRequested)
                                    {
                                        await channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                                        _logger.LogInformation($"消息已确认: {message}");
                                    }
                                    Console.WriteLine("消费成功");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "处理消息时发生错误");

                            // 处理失败，重新入队
                            if (channel.IsOpen && !cancellationToken.IsCancellationRequested)
                            {
                                await channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: true);
                                _logger.LogWarning($"消息已重新入队: {Encoding.UTF8.GetString(eventArgs.Body.ToArray())}");
                            }
                        }

                    };

                    // 4. 启动消费者
                    await channel.BasicConsumeAsync(
                         queue: queueName,
                         autoAck: false, // 手动确认
                         consumer: consumer
                     );

                    Console.WriteLine($"开始监听队列: {queueName}");
                    // 控制执行频率（重要！）
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"后台服务异常: {ex.Message}");
                _logger.LogError($"后台服务异常: {ex.Message}");
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (connection != null)
            {
                _connection.ReturnConnection(connection);
            }
            await base.StopAsync(cancellationToken);
        }
    }
}
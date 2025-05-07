using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VOL.Core.RabbitMQManager
{

    /// <summary>
    /// 
    /// </summary>
    public class BopConsumerService : BackgroundService
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;    
        private readonly string _routeKey;
        private readonly ILogger<BopConsumerService> _logger;

        public BopConsumerService(
            IRabbitMQConnection connection,
            IConfiguration config, ILogger<BopConsumerService> logger,
            IServiceProvider serviceProvider)
        {
            _logger= logger;
            _connection = connection;
            _config = config;
            _serviceProvider = serviceProvider;
            _routeKey = _config["RabbitMQ:RouteKey"];
        }

        /// <summary>
        /// 接收消息最推荐且最便捷的方式是使用接口设置订阅IAsyncBasicConsumer。
        /// 这样，消息到达时就会自动投递，无需主动请求。
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("BopConsumer-服务启动");
            _logger.LogInformation("服务启动");
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var channel = _connection.CreateChannel();

                    // 声明交换机
                  //  const string exchangeName = "sample.bop.direct";
                    channel.ExchangeDeclare(RabbitMQConfig.exchangeName, ExchangeType.Direct);

                    // 声明队列
                    var queueName = RabbitMQConfig.GetQueueName(_routeKey);
                   
                    //声明队列（确保存在）, 复用相同的队列配置
                    channel.QueueDeclare(
                        queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: RabbitMQConfig.QueueArguments);


                    // 设置QoS（避免消费者过载）消费者端的限流机制，它的核心作用是控制消费者同时处理的消息数量
                    channel.BasicQos(prefetchCount: 3, prefetchSize: 0, global: false);

                    // 绑定队列到交换机
                    channel.QueueBind(queueName, RabbitMQConfig.exchangeName, _routeKey);
                    
                    // 创建消费者
                    var consumer = new EventingBasicConsumer(channel);

                    // 注册消息处理函数
                    consumer.Received += async (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            await Task.CompletedTask;//Console.WriteLine($"接受订单[{message}] 来自供应商[{supplierId}]");
                                                     // 手动确认（必须关闭 autoAck）
                            channel.BasicAck(ea.DeliveryTag, multiple: false);
                        }
                        catch (Exception)
                        {
                            // 处理失败，重新入队
                            channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
                        }                                                               
                    };

                    await Task.Delay(500, stoppingToken);
                    // 启动消费者（关闭自动确认）
                    // 必须设为 false 才能手动确认
                    channel.BasicConsume(queue:queueName, autoAck:false, consumer: consumer);                   
                }
            }
            catch (Exception ex)
            {
                // 记录日志或处理异常
                Console.WriteLine($"后台服务异常: {ex.Message}");               
            }           
        }
    }
}
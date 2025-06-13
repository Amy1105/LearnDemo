using RabbitMQ.Client;
using System.Collections.Generic;

namespace VOL.Core.RabbitMQManager
{
    // RabbitMQConfig.cs
    public static class RabbitMQConfig
    {
        // 队列基础配置
       public  const string exchangeName = "sample.bop.direct";

        // 队列参数（自动转换为字典）
        public static readonly Dictionary<string, object?> QueueArguments = new Dictionary<string, object?>()
        {   
            { "x-queue-type",  "quorum" },
            { "x-message-ttl",  604800000 },      // 消息存活7天（毫秒）  604800000   7天，
            { "x-max-length", 10000 },           // 最大消息积压量  生产者限制：当队列中有10000条消息时，新消息会被拒绝（触发BasicReturn）
            { "x-dead-letter-exchange", "bop_dlx_exchange" }, // 死信交换机                                 
        };

        // 消息默认属性
        public static BasicProperties CreateDefaultProperties(IChannel channel, string routingKey)
        {
            return new BasicProperties
            {
                Persistent = true,  // 消息持久化,本质就是 DeliveryMode = 2                    
                Expiration = "604800000", // 消息存活7天（毫秒）
                ContentType = "application/json",                               
                Headers = new Dictionary<string, object?>
                {
                    { "country", routingKey },
                    { "version", "1.0" }
                }
            };
        }

        public static string GetQueueName(string _routeKey)
        {
            {
                return string.Format("queue_{0}_bop", _routeKey);
            }
        }
    }
}

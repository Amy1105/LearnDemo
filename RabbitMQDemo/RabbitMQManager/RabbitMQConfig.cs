using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace VOL.Core.RabbitMQManager
{
    // RabbitMQConfig.cs
    public static class RabbitMQConfig
    {
        // 队列基础配置
       public  const string exchangeName = "sample.bop.direct";

        // 队列参数（自动转换为字典）
        public static readonly Dictionary<string, object> QueueArguments = new Dictionary<string, object>()
        {
            { "x-message-ttl", 604800000 },      // 消息存活7天（毫秒）
            { "x-max-length", 10000 },           // 最大消息积压量  生产者限制：当队列中有10000条消息时，新消息会被拒绝（触发BasicReturn）
            { "x-dead-letter-exchange", "bop_dlx_exchange" }, // 死信交换机                      
            { "x-delivery-limit", 3 }            // 最大重试次数
        };

        // 消息默认属性
        public static IBasicProperties CreateDefaultProperties(IModel channel, string routingKey)
        {
            var props = channel.CreateBasicProperties();
            props.Persistent = true;             // 消息持久化,本质就是 DeliveryMode = 2
            props.Expiration = "604800000";   // 消息存活7天（毫秒）
            props.ContentType = "application/json";
            props.Headers = new Dictionary<string, object>
            {
                { "country", routingKey },
                { "version", "1.0" }
            };
            return props;
        }

        public static string GetQueueName(string _routeKey)
        {
            {
                return string.Format("queue_{0}_bop", _routeKey);
            }
        }
    }
}

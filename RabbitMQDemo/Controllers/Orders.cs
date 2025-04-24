using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Models;

namespace RabbitMQDemo.Controllers
{
    public class OrdersController : ControllerBase
    {
        private readonly IMessageProducer _producer;

        public OrdersController(IMessageProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            // 业务逻辑处理...

            // 发送订单创建事件
            _producer.SendMessage(new OrderCreatedEvent
            {
                OrderId = order.Id.ToString(),
                CustomerId = order.CustomerId.ToString(),
                TotalAmount = order.TotalAmount
            }, "order.created");

            return Ok();
        }
    }
}

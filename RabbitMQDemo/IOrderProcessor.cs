namespace RabbitMQDemo
{
    public interface IOrderProcessor
    {
        Task ProcessOrderAsync(OrderCreatedEvent orderEvent);
    }

    public class OrderProcessor : IOrderProcessor
    {
        private readonly ILogger<OrderProcessor> _logger;

        public OrderProcessor(ILogger<OrderProcessor> logger)
        {
            _logger = logger;
        }

        public async Task ProcessOrderAsync(OrderCreatedEvent orderEvent)
        {
            _logger.LogInformation($"Processing order {orderEvent.OrderId}");

            // 模拟处理时间
            await Task.Delay(1000);

            // 实际业务逻辑...
            _logger.LogInformation($"Order {orderEvent.OrderId} processed successfully");
        }
    }
}

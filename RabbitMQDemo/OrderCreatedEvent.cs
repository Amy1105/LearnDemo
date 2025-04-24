namespace RabbitMQDemo
{
    public class OrderCreatedEvent
    {
        public string OrderId { get; set; }         // 订单唯一标识
        public string CustomerId { get; set; }      // 客户ID
        public decimal TotalAmount { get; set; }    // 订单总金额
        public DateTime CreatedAt { get; set; }     // 创建时间
        public List<OrderItem> Items { get; set; }  // 订单项列表(可选)

        // 可以包含其他业务相关字段
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

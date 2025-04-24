using System.Runtime.CompilerServices;

namespace RabbitMQDemo.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }
    }
}

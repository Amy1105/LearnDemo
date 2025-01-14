using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DesignPatterns.StatePatterns
{
    /// <summary>
    /// 条件过多：每个操作都有多个检查条件。随着状态数量的增加，这些检查条件会变得更长，也更难维护。
    /// 逻辑分散：每个操作都必须了解所有可能的状态。这会把不同的逻辑混合在一处，使代码变得杂乱无章。
    /// 修改困难：如果我们添加更多的状态或操作，就必须更新代码中所有的 if-else 代码块。    
    /// </summary>
    public class Order
    {
        public string Status { get; set; } = "Pending";

        public void Pay()
        {
            if (Status == "Pending")
            {
                Status = "Paid";
                Console.WriteLine("Order has been paid.");
            }
            else
            {
                Console.WriteLine("Cannot pay for order in current state: " + Status);
            }
        }

        public void Ship()
        {
            if (Status == "Paid")
            {
                Status = "Shipped";
                Console.WriteLine("Order has been shipped.");
            }
            else
            {
                Console.WriteLine("Cannot ship order in current state: " + Status);
            }
        }

        public void Deliver()
        {
            if (Status == "Shipped")
            {
                Status = "Delivered";
                Console.WriteLine("Order has been delivered.");
            }
            else
            {
                Console.WriteLine("Cannot deliver order in current state: " + Status);
            }
        }

        public void Cancel()
        {
            if (Status == "Pending" || Status == "Paid")
            {
                Status = "Cancelled";
                Console.WriteLine("Order has been cancelled.");
            }
            else
            {
                Console.WriteLine("Cannot cancel order in current state: " + Status);
            }
        }
    }
}

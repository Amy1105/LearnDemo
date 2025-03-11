using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DesignPatterns.StatePatterns
{
    public class StatePatterns
    {
        public void execute()
        {
            var order = new NewOrder();
            order.Pay();      // 输出：Order has been paid.
            order.Ship();     // 输出：Order has been shipped.
            order.Deliver();  // 输出：Order has been delivered.
            order.Cancel();   // 输出：Cannot cancel a delivered order.
        }
    }
    public interface IOrderState
    {
        void Pay(NewOrder order);
        void Ship(NewOrder order);
        void Deliver(NewOrder order);
        void Cancel(NewOrder order);
    }

    public class PendingState : IOrderState
    {
        public void Pay(NewOrder order)
        {
            order.State = new PaidState();
            Console.WriteLine("Order has been paid.");
        }
        public void Ship(NewOrder order) => Console.WriteLine("Cannot ship a pending order.");
        public void Deliver(NewOrder order) => Console.WriteLine("Cannot deliver a pending order.");
        public void Cancel(NewOrder order)
        {
            //order.State = new CancelledState();
            Console.WriteLine("Order has been cancelled.");
        }
    }

    public class PaidState : IOrderState
    {
        public void Pay(NewOrder order) => Console.WriteLine("Order is already paid.");
        public void Ship(NewOrder order)
        {
            //order.State = new ShippedState();
            Console.WriteLine("Order has been shipped.");
        }
        public void Deliver(NewOrder order) => Console.WriteLine("Cannot deliver a paid order.");
        public void Cancel(NewOrder order)
        {
            //order.State = new CancelledState();
            Console.WriteLine("Order has been cancelled.");
        }
    }

    public class NewOrder
    {
        public IOrderState State { get; set; } = new PendingState();

        public void Pay() => State.Pay(this);
        public void Ship() => State.Ship(this);
        public void Deliver() => State.Deliver(this);
        public void Cancel() => State.Cancel(this);
    }
}

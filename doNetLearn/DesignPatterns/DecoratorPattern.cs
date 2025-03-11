using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DesignPatterns
{
    /// <summary>
    /// 装饰器模式（Decorator Pattern）就是这样一种强大的模式，它属于结构型设计模式类别。
    /// 如果你曾受困于继承的僵化性，或者遇到过需要扩展对象行为却又不想改变其核心结构的情况，那么装饰器模式就是你正在寻找的解决方案
    /// </summary>
    internal class DecoratorPattern
    {
        /// <summary>
        /// 装饰器模式是一种结构型设计模式，它允许你在不改变对象结构的情况下动态地为对象添加新功能。
        /// 它为扩展功能提供了一种比继承更灵活的替代方案。装饰器模式使用组合而非继承的方式——用另一个对象（装饰器）来包装一个对象，
        /// 以此扩展其行为
        /// </summary>
        public void execute()
        {
            /* 优点：
            灵活性：与静态的继承不同，装饰器模式使你能够在运行时为对象添加行为。
            遵循开闭原则：你可以在不修改现有代码的基础上扩展对象的功能。
            避免继承开销：无需为不同的行为组合创建多个子类，而是可以根据需要组合装饰器             
             */

            ICoffee coffee = new SimpleCoffee();
            Console.WriteLine($"{coffee.GetDescription()} costs {coffee.GetCost():C}");

            coffee = new MilkDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} costs {coffee.GetCost():C}");

            coffee = new SugarDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} costs {coffee.GetCost():C}");

            /*
            增加复杂性： 该模式引入了额外的类，这可能会使代码更难理解和维护。
            可能被过度使用： 过度使用装饰器可能会导致一长串的包装器，使得调试和追踪行为变得困难。
            依赖抽象： 对组件接口的更改可能需要对所有装饰器进行相应更改。
             */
        }
    }

    public interface ICoffee
    {
        string GetDescription();
        double GetCost();
    }

    public class SimpleCoffee : ICoffee
    {
        public string GetDescription()
        {
            return "Simple Coffee";
        }

        public double GetCost()
        {
            return 2.00; // 原味咖啡的基础价格
        }
    }

    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public virtual string GetDescription()
        {
            return _coffee.GetDescription();
        }

        public virtual double GetCost()
        {
            return _coffee.GetCost();
        }
    }

    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", Milk";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 0.50;
        }
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", Sugar";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 0.20;
        }
    }
}

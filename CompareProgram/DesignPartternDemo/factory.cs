using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPartternDemo.Factory
{
    public interface Shape
    {
        void draw();
    }

    public class Rectangle : Shape
    {


        public void draw()
        {
            Console.WriteLine("Inside Rectangle::draw() method.");
        }
    }

    public class Square : Shape
    {
        public void draw()
        {
            Console.WriteLine("Inside Square::draw() method.");
        }
    }

    public class Circle : Shape
    {

        public void draw()
        {
            Console.WriteLine("Inside Circle::draw() method.");
        }
    }

    public class ShapeFactory
    {

        //使用 getShape 方法获取形状类型的对象
        public Shape getShape(String shapeType)
        {
            if (shapeType == null)
            {
                return null;
            }
            if ("CIRCLE".Equals(shapeType))
            {
                return new Circle();
            }
            else if ("RECTANGLE".Equals(shapeType))
            {
                return new Rectangle();
            }
            else if ("SQUARE".Equals(shapeType))
            {
                return new Square();
            }
            return null;
        }
    }

    /// <summary>
    /// 工厂模式
    /// </summary>
    public class FactoryPatternDemo
    {
        public static void Method()
        {
            ShapeFactory shapeFactory = new ShapeFactory();

            //获取 Circle 的对象，并调用它的 draw 方法
            Shape shape1 = shapeFactory.getShape("CIRCLE");

            //调用 Circle 的 draw 方法
            shape1.draw();

            //获取 Rectangle 的对象，并调用它的 draw 方法
            Shape shape2 = shapeFactory.getShape("RECTANGLE");

            //调用 Rectangle 的 draw 方法
            shape2.draw();

            //获取 Square 的对象，并调用它的 draw 方法
            Shape shape3 = shapeFactory.getShape("SQUARE");

            //调用 Square 的 draw 方法
            shape3.draw();
        }
    }
}

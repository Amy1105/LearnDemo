using DesignPartternDemo.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPartternDemo.AbstractFactory
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

    public interface Color
    {
        void fill();
    }

    public class Red : Color
    {

        public void fill()
        {
            Console.WriteLine("Inside Red::fill() method.");
        }
    }

    public class Green : Color
    {


        public void fill()
        {
            Console.WriteLine("Inside Green::fill() method.");
        }
    }

    public class Blue : Color
    {

        public void fill()
        {
            Console.WriteLine("Inside Blue::fill() method.");
        }
    }
    public abstract class AbstractFactory
    {
        public abstract Color getColor(String color);
        public abstract Shape getShape(String shape);
    }

    public class ShapeFactory : AbstractFactory
    {


        public override Shape getShape(String shapeType)
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


        public override Color getColor(String color)
        {
            return null;
        }
    }

    public class ColorFactory : AbstractFactory
    {
        public override Shape getShape(String shapeType)
        {
            return null;
        }

        public override Color getColor(String color)
        {
            if (color == null)
            {
                return null;
            }
            if ("RED".Equals(color))
            {
                return new Red();
            }
            else if ("GREEN".Equals(color))
            {
                return new Green();
            }
            else if ("BLUE".Equals(color))
            {
                return new Blue();
            }
            return null;
        }
    }
    public class FactoryProducer
    {
        public static AbstractFactory getFactory(String choice)
        {
            if ("SHAPE".Equals(choice))
            {
                return new ShapeFactory();
            }
            else if ("COLOR".Equals(choice))
            {
                return new ColorFactory();
            }
            return null;
        }
    }

    public class AbstractFactoryPatternDemo
    {
        public static void main(String[] args)
        {

            //获取形状工厂
            AbstractFactory shapeFactory = FactoryProducer.getFactory("SHAPE");

            //获取形状为 Circle 的对象
            Shape shape1 = shapeFactory.getShape("CIRCLE");

            //调用 Circle 的 draw 方法
            shape1.draw();

            //获取形状为 Rectangle 的对象
            Shape shape2 = shapeFactory.getShape("RECTANGLE");

            //调用 Rectangle 的 draw 方法
            shape2.draw();

            //获取形状为 Square 的对象
            Shape shape3 = shapeFactory.getShape("SQUARE");

            //调用 Square 的 draw 方法
            shape3.draw();

            //获取颜色工厂
            AbstractFactory colorFactory = FactoryProducer.getFactory("COLOR");

            //获取颜色为 Red 的对象
            Color color1 = colorFactory.getColor("RED");

            //调用 Red 的 fill 方法
            color1.fill();

            //获取颜色为 Green 的对象
            Color color2 = colorFactory.getColor("GREEN");

            //调用 Green 的 fill 方法
            color2.fill();

            //获取颜色为 Blue 的对象
            Color color3 = colorFactory.getColor("BLUE");

            //调用 Blue 的 fill 方法
            color3.fill();
        }
    }
}

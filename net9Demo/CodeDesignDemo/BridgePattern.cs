using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.CodeDesignDemo
{
    public interface DrawAPI
    {
        public void drawCircle(int radius, int x, int y);
    }

    public class RedCircle : DrawAPI
    {
        public void drawCircle(int radius, int x, int y)
        {
            Console.Write("Drawing Circle[ color: red, radius: "
               + radius + ", x: " + x + ", " + y + "]");
        }
    }

    public class GreenCircle : DrawAPI
    {
        public void drawCircle(int radius, int x, int y)
        {
            Console.Write("Drawing Circle[ color: green, radius: "
           + radius + ", x: " + x + ", " + y + "]");
        }
    }

    /// <summary>
    /// 用于将抽象部分与实现部分分离，使得它们可以独立地变化,
    /// 避免使用继承导致的类爆炸问题，提供更灵活的扩展方式
    /// </summary>
    public abstract class Shape
    {
        protected DrawAPI drawAPI;
        protected Shape(DrawAPI drawAPI)
        {
            this.drawAPI = drawAPI;
        }
        public abstract void draw();
    }

    public class Circle : Shape
    {
        private int x, y, radius;

        public Circle(int x, int y, int radius, DrawAPI drawAPI) : base(drawAPI)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public override void draw()
        {
            drawAPI.drawCircle(radius, x, y);
        }
    }

    public class BridgePatternDemo
    {
        public static void Test()
        {
            Shape redCircle = new Circle(100, 100, 10, new RedCircle());
            Shape greenCircle = new Circle(100, 100, 10, new GreenCircle());

            redCircle.draw();
            greenCircle.draw();
        }
    }
}
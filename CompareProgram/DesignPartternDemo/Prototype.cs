using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPartternDemo.Prototype
{
    public abstract class Shape : Cloneable
    {


   private String id;
    protected String type;

    abstract void draw();

    public String getType()
    {
        return type;
    }

    public String getId()
    {
        return id;
    }

    public void setId(String id)
    {
        this.id = id;
    }

    public Object clone()
    {
        Object clone = null;
        try
        {
            clone = base.clone();
        }
        catch (Exception  e)
        {
                throw e;
        }
        return clone;
    }
}

    public class Rectangle : Shape
    {


   public Rectangle()
    {
        type = "Rectangle";
    }

   public void draw()
    {
            Console.WriteLine("Inside Rectangle::draw() method.");
    }
}

    public class Square : Shape
    {


   public Square()
    {
        type = "Square";
    }

   public void draw()
    {
            Console.WriteLine("Inside Square::draw() method.");
    }
}

    public class Circle : Shape
    {


   public Circle()
    {
        type = "Circle";
    }

   public void draw()
    {
            Console.WriteLine("Inside Circle::draw() method.");
    }
}

    public class ShapeCache
    {
        private static Dictionary<String, Shape> shapeMap
           = new Dictionary<String, Shape>();

        public static Shape getShape(String shapeId)
        {
            Shape cachedShape = shapeMap.get(shapeId);
            return (Shape)cachedShape.clone();
        }

        // 对每种形状都运行数据库查询，并创建该形状
        // shapeMap.put(shapeKey, shape);
        // 例如，我们要添加三种形状
        public static void loadCache()
        {
            Circle circle = new Circle();
            circle.setId("1");
            shapeMap.put(circle.getId(), circle);

            Square square = new Square();
            square.setId("2");
            shapeMap.put(square.getId(), square);

            Rectangle rectangle = new Rectangle();
            rectangle.setId("3");
            shapeMap.put(rectangle.getId(), rectangle);
        }
    }

    public class PrototypePatternDemo
    {
        public static void main(String[] args)
        {
            ShapeCache.loadCache();

            Shape clonedShape = (Shape)ShapeCache.getShape("1");
            Console.WriteLine("Shape : " + clonedShape.getType());

            Shape clonedShape2 = (Shape)ShapeCache.getShape("2");
            Console.WriteLine("Shape : " + clonedShape2.getType());

            Shape clonedShape3 = (Shape)ShapeCache.getShape("3");
            Console.WriteLine("Shape : " + clonedShape3.getType());
        }
    }
}

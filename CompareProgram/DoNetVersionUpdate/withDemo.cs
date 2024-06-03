using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetVersionUpdate
{
    /// <summary>
    /// with-- 非破坏性突变创建具有修改属性的新对象
    /// with 表达式使用修改的特定属性和字段生成其操作数的副本。 使用对象初始值设定项语法来指定要修改的成员及其新值
    /// </summary>
    internal class withDemo
    {
        public record Point(int X, int Y);
        public record NamedPoint(string Name, int X, int Y) : Point(X, Y);

        public static void Method1()
        {
            var p1 = new NamedPoint("A", 0, 0);
            Console.WriteLine($"{nameof(p1)}: {p1}");  // output: p1: NamedPoint { Name = A, X = 0, Y = 0 }

            var p2 = p1 with { Name = "B", X = 5 };
            Console.WriteLine($"{nameof(p2)}: {p2}");  // output: p2: NamedPoint { Name = B, X = 5, Y = 0 }

            var p3 = p1 with
            {
                Name = "C",
                Y = 4
            };
            Console.WriteLine($"{nameof(p3)}: {p3}");  // output: p3: NamedPoint { Name = C, X = 0, Y = 4 }

            Console.WriteLine($"{nameof(p1)}: {p1}");  // output: p1: NamedPoint { Name = A, X = 0, Y = 0 }

            var apples = new { Item = "Apples", Price = 1.19m };
            Console.WriteLine($"Original: {apples}");  // output: Original: { Item = Apples, Price = 1.19 }
            var saleApples = apples with { Price = 0.79m };
            Console.WriteLine($"Sale: {saleApples}");  // output: Sale: { Item = Apples, Price = 0.79 }
        }

        public static void Method2()
        {
            Point p1 = new NamedPoint("A", 0, 0);
            Point p2 = p1 with { X = 5, Y = 3 };
            Console.WriteLine(p1.GetHashCode());
            Console.WriteLine(p2.GetHashCode());
            Console.WriteLine(p2 is NamedPoint);  // output: True
            Console.WriteLine(p2);  // output: NamedPoint { X = 5, Y = 3, Name = A }
        }
    }
}

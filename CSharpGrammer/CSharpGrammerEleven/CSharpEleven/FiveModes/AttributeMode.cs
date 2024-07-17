using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammerEleven.CSharpEleven.FiveModes
{
    /// <summary>
    /// 属性模式
    /// </summary>
    public partial class AttributeMode
    {
        public static void Method()
        {
            //可以使用属性模式将表达式的属性或字段与嵌套模式进行匹配
            static bool IsConferenceDay(DateTime date) => date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 };


            //还可将运行时类型检查和变量声明添加到属性模式
            Console.WriteLine(TakeFive("Hello, world!"));  // output: Hello
            Console.WriteLine(TakeFive("Hi!"));  // output: Hi!
            Console.WriteLine(TakeFive(new[] { '1', '2', '3', '4', '5', '6', '7' }));  // output: 12345
            Console.WriteLine(TakeFive(new[] { 'a', 'b', 'c' }));  // output: abc

            static string TakeFive(object input) => input switch
            {
                string { Length: >= 5 } s => s.Substring(0, 5),
                string s => s,

                ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
                ICollection<char> symbols => new string(symbols.ToArray()),

                null => throw new ArgumentNullException(nameof(input)),
                _ => throw new ArgumentException("Not supported input type."),
            };
        }
    }

    public partial class Attribute
    {
        //属性模式是一种递归模式。 也就是说，可以将任何模式用作嵌套模式。 使用属性模式将部分数据与嵌套模式进行匹配
        public record Point(int X, int Y);
        public record Segment(Point Start, Point End);

        static bool IsAnyEndOnXAxis(Segment segment) =>
            segment is { Start: { Y: 0 } } or { End: { Y: 0 } };



        //上一示例使用 or模式连结符和记录类型。
        //从 C# 10 开始，可引用属性模式中嵌套的属性或字段。 该功能称为“扩展属性模式”。 例如，可将上述示例中的方法重构为以下等效代码：
        static bool IsAnyEndOnXAxis2(Segment segment) =>
    segment is { Start.Y: 0 } or { End.Y: 0 };
    }
}

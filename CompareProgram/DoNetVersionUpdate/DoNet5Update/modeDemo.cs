using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DoNetVersionUpdate.DoNet5Update
{
    /// <summary>
    /// 模式匹配
    ///  以下 C# 表达式和语句支持模式匹配：is表达式、switch 语句、switch 表达式    
    /// </summary>
    internal class modeDemo
    {
        #region 声明模式 ：用于检查表达式的运行时类型，如果匹配成功，则将表达式结果分配给声明的变量。类型模式 用于检查表达式的运行时类型

        public static void Method()
        {
            object greeting = "Hello, World!";
            if (greeting is string message)
            {
                Console.WriteLine(message.ToLower());  // output: hello, world!
            }
        }

        /// <summary>
        ///  表达式结果的运行时类型为 T。
        ///  表达式结果的运行时类型派生自类型 T，实现接口 T，或者存在从其到 T 的另一种隐式引用转换。
        /// </summary>
        public static void Method2()
        {
            var numbers = new int[] { 10, 20, 30 };
            Console.WriteLine(GetSourceLabel(numbers));  // output: 1

            var letters = new List<char> { 'a', 'b', 'c', 'd' };
            Console.WriteLine(GetSourceLabel(letters));  // output: 2

            static int GetSourceLabel<T>(IEnumerable<T> source) => source switch
            {
                Array array => 1,
                ICollection<T> collection => 2,
                _ => 3,
            };
        }

        public static void Method3()
        {
            int? xNullable = 7;
            int y = 23;
            object yBoxed = y;
            if (xNullable is int a && yBoxed is int b)
            {
                Console.WriteLine(a + b);  // output: 30
            }
        }

        #endregion

        #region 常量模式 ：用于测试表达式结果是否等于指定常量

        /// <summary>
        /// 在常量模式中，可使用任何常量表达式
        ///     integer 或 floating-point 数值文本
        ///     字符型
        ///     字符串字面量。
        ///     布尔值 true 或 false
        ///     enum 值
        ///     声明常量字段或本地的名称
        ///     null
        /// </summary>
        /// <param name="visitorCount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static decimal GetGroupTicketPrice(int visitorCount) => visitorCount switch
        {
            1 => 12.0m,
            2 => 20.0m,
            3 => 27.0m,
            4 => 32.0m,
            0 => 0.0m,
            _ => throw new ArgumentException($"Not supported number of visitors: {visitorCount}", nameof(visitorCount)),
        };

        /// <summary>
        /// 表达式类型必须可转换为常量类型，
        /// 但有一个例外：类型为 Span<char> 或 ReadOnlySpan<char> 的表达式可以在 C# 11 及更高版本中针对常量字符串进行匹配
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static int ToNum(ReadOnlySpan<char> s)
        {
            return s switch
            {
                { Length: 0 } => 0,
                "" => 1,        // error: unreachable?
                ['A', ..] => 2,
                "ABC" => 3,     // error: unreachable?
                _ => 4,
            };
        }

        #endregion

        #region 关系模式 ：用于将表达式结果与指定常量进行比较

        /// <summary>
        /// 在关系模式中，可使用关系运算符<、>、<= 或 >= 中的任何一个。 
        /// 关系模式的右侧部分必须是常数表达式。 
        /// 常数表达式可以是 integer、floating-point、char 或 enum 类型
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns></returns>
        static string Classify(double measurement) => measurement switch
        {
            < -4.0 => "Too low",
            > 10.0 => "Too high",
            double.NaN => "Unknown",
            _ => "Acceptable",
        };

        public static void Method4()
        {
            Console.WriteLine(Classify(13));  // output: Too high
            Console.WriteLine(Classify(double.NaN));  // output: Unknown
            Console.WriteLine(Classify(2.4));  // output: Acceptable

            //要检查表达式结果是否在某个范围内，请将其与合取 and 模式匹配
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 3, 14)));  // output: spring
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 7, 19)));  // output: summer
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 2, 17)));  // output: winter

        }


        static string GetCalendarSeason(DateTime date) => date.Month switch
        {
            >= 3 and < 6 => "spring",
            >= 6 and < 9 => "summer",
            >= 9 and < 12 => "autumn",
            12 or (>= 1 and < 3) => "winter",
            _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
        };
        #endregion

        #region 逻辑模式 ：用于测试表达式是否与模式的逻辑组合匹配
        /// <summary>
        /// 请使用 not、and 和 or 模式连结符来创建以下逻辑模式   
        /// 检查的优先级和顺序:
        ///        not
        ///        and
        ///        or
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        static string GetCalendarSeason2(DateTime date) => date.Month switch
        {
            3 or 4 or 5 => "spring",
            6 or 7 or 8 => "summer",
            9 or 10 or 11 => "autumn",
            12 or 1 or 2 => "winter",
            _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
        };

        public static void Method5()
        {
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 1, 19)));  // output: winter
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 10, 9)));  // output: autumn
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 5, 11)));  // output: spring

            //检查的优先级和顺序:not  and  or
            bool IsLetter(char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');
            Console.WriteLine(IsLetter('f'));
        }



        #endregion
        #region 属性模式 ：用于测试表达式的属性或字段是否与嵌套模式匹配


        static bool IsConferenceDay(DateTime date) => date is { Year: 2024, Month: 6, Day: 4 or 5 or 6 };

        static string TakeFive(object input) => input switch
        {
            string { Length: >= 5 } s => s.Substring(0, 5),
            string s => s,

            ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
            ICollection<char> symbols => new string(symbols.ToArray()),

            null => throw new ArgumentNullException(nameof(input)),
            _ => throw new ArgumentException("Not supported input type."),
        };

        public static void Method6()
        {
            IsConferenceDay(DateTime.UtcNow);

            Console.WriteLine(TakeFive("Hello, world!"));  // output: Hello
            Console.WriteLine(TakeFive("Hi!"));  // output: Hi!
            Console.WriteLine(TakeFive(new[] { '1', '2', '3', '4', '5', '6', '7' }));  // output: 12345
            Console.WriteLine(TakeFive(new[] { 'a', 'b', 'c' }));  // output: abc

            //属性模式是一种递归模式。 也就是说，可以将任何模式用作嵌套模式。 使用属性模式将部分数据与嵌套模式进行匹配

            var s1 = new Segment(new Point(5, 6), new Point(6, 0));
            Console.WriteLine(IsAnyEndOnXAxis(s1));
        }

        public record Point(int X, int Y);
        public record Segment(Point Start, Point End);

        static bool IsAnyEndOnXAxis(Segment segment) =>
            segment is { Start: { Y: 0 } } or { End: { Y: 0 } };


        #endregion
        #region 位置模式 ：用于解构表达式结果并测试结果值是否与嵌套模式匹配

        public readonly struct Point2
        {
            public int X { get; }
            public int Y { get; }

            public Point2(int x, int y) => (X, Y) = (x, y);

            public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
        }

      public  static string Classify(Point2 point) => point switch
        {
            (0, 0) => "Origin",
            (1, 0) => "positive X basis end",
            (0, 1) => "positive Y basis end",
            _ => "Just a point",
        };

        public static decimal GetGroupTicketPriceDiscount(int groupSize, DateTime visitDate)
    => (groupSize, visitDate.DayOfWeek) switch
    {
        ( <= 0, _) => throw new ArgumentException("Group size must be positive."),
        (_, DayOfWeek.Saturday or DayOfWeek.Sunday) => 0.0m,
        ( >= 5 and < 10, DayOfWeek.Monday) => 20.0m,
        ( >= 10, DayOfWeek.Monday) => 30.0m,
        ( >= 5 and < 10, _) => 12.0m,
        ( >= 10, _) => 15.0m,
        _ => 0.0m,
    };


        static (double Sum, int Count) SumAndCount(IEnumerable<int> numbers)
        {
            int sum = 0;
            int count = 0;
            foreach (int number in numbers)
            {
                sum += number;
                count++;
            }
            return (sum, count);
        }


        public static void Method7()
        {
            var numbers = new List<int> { 1, 2, 3 };
            if (SumAndCount(numbers) is (Sum: var sum, Count: > 0))
            {
                Console.WriteLine($"Sum of [{string.Join(" ", numbers)}] is {sum}");  // output: Sum of [1 2 3] is 6
            }

            //

            //if (input is WeightedPoint(> 0, > 0) { Weight: > 0.0 } p)
            //{
            //    // ..
            //}
        }


        public record Point2D(int X, int Y);
        public record Point3D(int X, int Y, int Z);

        static string PrintIfAllCoordinatesArePositive(object point) => point switch
        {
            Point2D(> 0, > 0) p => p.ToString(),
            Point3D(> 0, > 0, > 0) p => p.ToString(),
            _ => string.Empty,
        };

        public record WeightedPoint(int X, int Y)
        {
            public double Weight { get; set; }
        }

        static bool IsInDomain(WeightedPoint point) => point is ( >= 0, >= 0) { Weight: >= 0.0 };

        #endregion
        #region var 模式 ：用于匹配任何表达式并将其结果分配给声明的变量
        //可使用 var 模式来匹配任何表达式（包括 null），并将其结果分配给新的局部变量
      public  static bool IsAcceptable(int id, int absLimit) =>
    SimulateDataFetch(id) is var results
    && results.Min() >= -absLimit
    && results.Max() <= absLimit;

        static int[] SimulateDataFetch(int id)
        {
            var rand = new Random();
            return Enumerable
                       .Range(start: 0, count: 5)
                       .Select(s => rand.Next(minValue: -10, maxValue: 11))
                       .ToArray();
        }

        public record Point3(int X, int Y);

        static Point3 Transform(Point3 point) => point switch
        {
            //模式 var (x, y) 等效于位置模式(var x, var y)
            var (x, y) when x < y => new Point3(-x, y),
            var (x, y) when x > y => new Point3(x, -y),
            var (x, y) => new Point3(x, y),
        };

        static void TestTransform()
        {
            Console.WriteLine(Transform(new Point3(1, 2)));  // output: Point { X = -1, Y = 2 }
            Console.WriteLine(Transform(new Point3(5, 2)));  // output: Point { X = 5, Y = -2 }
        }

        #endregion
        #region 弃元模式 ：用于匹配任何表达式        
        static decimal GetDiscountInPercent(DayOfWeek? dayOfWeek) => dayOfWeek switch
        {
            DayOfWeek.Monday => 0.5m,
            DayOfWeek.Tuesday => 12.5m,
            DayOfWeek.Wednesday => 7.5m,
            DayOfWeek.Thursday => 12.5m,
            DayOfWeek.Friday => 5.0m,
            DayOfWeek.Saturday => 2.5m,
            DayOfWeek.Sunday => 2.0m,
            _ => 0.0m,
        };

        public static void Method8()
        {
            Console.WriteLine(GetDiscountInPercent(DayOfWeek.Friday));  // output: 5.0
            Console.WriteLine(GetDiscountInPercent(null));  // output: 0.0
            Console.WriteLine(GetDiscountInPercent((DayOfWeek)10));  // output: 0.0

            #region 带括号模式

            //可在任何模式两边加上括号。 通常，这样做是为了强调或更改逻辑模式中的优先级
            //if (input is not (float or double))
            //{
            //    return;
            //}
            #endregion
        }

        #endregion

        #region 列表模式 :测试序列元素是否与相应的嵌套模式匹配。 在 C# 11 中引入。
        public static void Method9()
        {
            int[] numbers = { 1, 2, 3 };
            Console.WriteLine(numbers is [1, 2, 3]);  // True
            Console.WriteLine(numbers is [1, 2, 4]);  // False
            Console.WriteLine(numbers is [1, 2, 3, 4]);  // False
            Console.WriteLine(numbers is [0 or 1, <= 2, >= 3]);  // True
           
            if (numbers is [var first, _, _])
            {
                Console.WriteLine($"The first element of a three-item list is {first}.");
            }
            // Output:
            // The first element of a three-item list is 1.

            Console.WriteLine(new[] { 1, 2, 3, 4, 5 } is [> 0, > 0, ..]);  // True
            Console.WriteLine(new[] { 1, 1 } is [_, _, ..]);  // True
            Console.WriteLine(new[] { 0, 1, 2, 3, 4 } is [> 0, > 0, ..]);  // False
            Console.WriteLine(new[] { 1 } is [1, 2, ..]);  // False

            Console.WriteLine(new[] { 1, 2, 3, 4 } is [.., > 0, > 0]);  // True
            Console.WriteLine(new[] { 2, 4 } is [.., > 0, 2, 4]);  // False
            Console.WriteLine(new[] { 2, 4 } is [.., 2, 4]);  // True

            Console.WriteLine(new[] { 1, 2, 3, 4 } is [>= 0, .., 2 or 4]);  // True
            Console.WriteLine(new[] { 1, 0, 0, 1 } is [1, 0, .., 0, 1]);  // True
            Console.WriteLine(new[] { 1, 0, 1 } is [1, 0, .., 0, 1]);  // False


            MatchMessage("aBBA");  // output: Message aBBA matches; inner part is BB.
            MatchMessage("apron");  // output: Message apron doesn't match.

            Validate(new[] { -1, 0, 1 });  // output: not valid
            Validate(new[] { -1, 0, 0, 1 });  // output: valid
        }
        ///还可以在切片模式中嵌套子模式
        public static void MatchMessage(string message)
        {
            var result = message is ['a' or 'A', ..var s, 'a' or 'A']
                ? $"Message {message} matches; inner part is {s}."
                : $"Message {message} doesn't match.";
            Console.WriteLine(result);
        }


        static void Validate(int[] numbers)
        {
            var result = numbers is [< 0, .. { Length: 2 or 4 }, > 0] ? "valid" : "not valid";
            Console.WriteLine(result);
        }

        #endregion

        #region C# 语言规范


        #region 递归模式匹配
        #endregion
        #region 模式匹配更新
        #endregion
        #region  C# 10 - 扩展属性模式
        #endregion
        #region  C# 11 - 列出模式
        #endregion
        #region   C# 11 - 字符串字面量上的模式匹配 Span<char>
        #endregion

        #endregion
    }

    public abstract class Vehicle { }
    public class Car : Vehicle { }
    public class Truck : Vehicle { }
    public static class TollCalculator
    {
        public static decimal CalculateToll(this Vehicle vehicle) => vehicle switch
        {
            Car _ => 2.00m,
            Truck _ => 7.50m,
            null => throw new ArgumentNullException(nameof(vehicle)),
            _ => throw new ArgumentException("Unknown type of a vehicle", nameof(vehicle)),
        };
        public static decimal CalculateToll2(this Vehicle vehicle) => vehicle switch
        {
            Car => 2.00m,
            Truck => 7.50m,
            null => throw new ArgumentNullException(nameof(vehicle)),
            _ => throw new ArgumentException("Unknown type of a vehicle", nameof(vehicle)),
        };
    }
}
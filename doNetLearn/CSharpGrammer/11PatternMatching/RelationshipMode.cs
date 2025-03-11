using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammerEleven.CSharpEleven.FiveModes
{
    /// <summary>
    /// 关系模式
    /// </summary>
    public class RelationshipMode
    {

        public static void Method()
        {
            Console.WriteLine(Classify(13));  // output: Too high
            Console.WriteLine(Classify(double.NaN));  // output: Unknown
            Console.WriteLine(Classify(2.4));  // output: Acceptable

            static string Classify(double measurement) => measurement switch
            {
                < -4.0 => "Too low",
                > 10.0 => "Too high",
                double.NaN => "Unknown",
                _ => "Acceptable",
            };
        }

        /// <summary>
        /// 在关系模式中，可使用关系运算符<、>、<= 或 >= 中的任何一个。 关系模式的右侧部分必须是常数表达式。 
        /// 常数表达式可以是 integer、floating-point、char 或 enum 类型。
        /// 要检查表达式结果是否在某个范围内，请将其与合取 and 模式匹配
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Method2()
        {
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 3, 14)));  // output: spring
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 7, 19)));  // output: summer
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 2, 17)));  // output: winter

            static string GetCalendarSeason(DateTime date) => date.Month switch
            {
                >= 3 and < 6 => "spring",
                >= 6 and < 9 => "summer",
                >= 9 and < 12 => "autumn",
                12 or (>= 1 and < 3) => "winter",
                _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
            };
        }
    }
}

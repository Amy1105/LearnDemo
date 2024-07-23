using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammerEleven.CSharpEleven.FiveModes
{
    /// <summary>
    /// 逻辑模式:请使用 not、and 和 or 模式连结符来创建以下逻辑模式
    /// </summary>
    public class LogicMode
    {
        public static void Method()
        {
            //否定 not 模式在否定模式与表达式不匹配时与表达式匹配
            int? input = 9;
            if (input is not null)
            {
                // ...
            }


            //合取 and 模式在两个模式都与表达式匹配时与表达式匹配
            Console.WriteLine(Classify(13));  // output: High
            Console.WriteLine(Classify(-100));  // output: Too low
            Console.WriteLine(Classify(5.7));  // output: Acceptable

            static string Classify(double measurement) => measurement switch
            {
                < -40.0 => "Too low",
                >= -40.0 and < 0 => "Low",
                >= 0 and < 10.0 => "Acceptable",
                >= 10.0 and < 20.0 => "High",
                >= 20.0 => "Too high",
                double.NaN => "Unknown",
            };



            //析取 or 模式在任一模式与表达式匹配时与表达式匹配
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 1, 19)));  // output: winter
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 10, 9)));  // output: autumn
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 5, 11)));  // output: spring

            static string GetCalendarSeason(DateTime date) => date.Month switch
            {
                3 or 4 or 5 => "spring",
                6 or 7 or 8 => "summer",
                9 or 10 or 11 => "autumn",
                12 or 1 or 2 => "winter",
                _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
            };


            //模式组合器按从最高优先级到最低优先级排序:not  and  or    
            static bool IsLetter(char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');
        }


    }
}

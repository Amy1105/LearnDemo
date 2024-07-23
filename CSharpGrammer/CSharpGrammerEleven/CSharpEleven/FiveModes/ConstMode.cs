using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammerEleven.CSharpEleven.FiveModes
{
    /// <summary>
    /// 常量模式:可使用常量模式来测试表达式结果是否等于指定的常量
    /// 在常量模式中，可使用任何常量表达式，例如：integer 或 floating-point 数值文本、字符型、字符串字面量、布尔值 true 或 fals、enum 值、声明常量字段或本地的名称、null  
    /// </summary>
    public class ConstMode
    {

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
        /// 常量模式 比较null
        /// </summary>
        public static void Method()
        {
            {
                //若要检查非 null，可使用否定null常量模式
                int? input = 7;
                if (input is not null)
                {
                    Console.WriteLine($"input:{input}");
                }

                if (input is  null)
                {
                    Console.WriteLine($"input:{input}");
                }
            }
        }
    }
}

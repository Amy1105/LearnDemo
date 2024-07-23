using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammerEleven.CSharpEleven.FiveModes
{
    /// <summary>
    /// 5.列表模式扩展了模式匹配，以匹配列表或数组中的元素序列
    /// </summary>
    public class FiveMode
    {
        /// <summary>
        /// 从 C# 11 开始，可以将数组或列表与模式的序列进行匹配
        /// </summary>
        public static void Method()
        {
            int[] numbers = { 1, 2, 3 };

            Console.WriteLine(numbers is [1, 2, 3]);  // True
            Console.WriteLine(numbers is [1, 2, 4]);  // False
            Console.WriteLine(numbers is [1, 2, 3, 4]);  // False
            Console.WriteLine(numbers is [0 or 1, <= 2, >= 3]);  // True
        }

        /// <summary>
        /// 若要匹配任何元素，请使用弃元模式
        /// </summary>
        public static void Method2()
        {
            List<int> numbers = new() { 1, 2, 3 };

            if (numbers is [var first, _, _])
            {
                Console.WriteLine($"The first element of a three-item list is {first}.");
            }
            // Output:
            // The first element of a three-item list is 1.
        }

        /// <summary>
        /// 如果还想捕获元素，请使用 var 模式
        /// </summary>
        public static void Method3()
        {
            List<int> numbers = new() { 1, 2, 3 };

            if (numbers is [var first, _, _])
            {
                Console.WriteLine($"The first element of a three-item list is {first}.");
            }
            // Output:
            // The first element of a three-item list is 1.
        }

        /// <summary>
        /// 前面的示例将整个输入序列与列表模式匹配。 若要仅匹配输入序列开头或/和结尾的元素，请使用切片模式.
        /// 切片模式匹配零个或多个元素。 最多可在列表模式中使用一个切片模式。 切片模式只能显示在列表模式中
        /// </summary>
        public static void Method4()
        {
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
        }

        /// <summary>
        /// 还可以在切片模式中嵌套子模式
        /// </summary>
        public static void Method5()
        {
            void MatchMessage(string message)
            {
                var result = message is ['a' or 'A', .. var s, 'a' or 'A']
                    ? $"Message {message} matches; inner part is {s}."
                    : $"Message {message} doesn't match.";
                Console.WriteLine(result);
            }

            MatchMessage("aBBA");  // output: Message aBBA matches; inner part is BB.
            MatchMessage("apron");  // output: Message apron doesn't match.

            void Validate(int[] numbers)
            {
                var result = numbers is [< 0, .. { Length: 2 or 4 }, > 0] ? "valid" : "not valid";
                Console.WriteLine(result);
            }

            Validate(new[] { -1, 0, 1 });  // output: not valid
            Validate(new[] { -1, 0, 0, 1 });  // output: valid
        }
    }


}

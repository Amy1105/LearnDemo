using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammerEleven.CSharpEleven.FiveModes
{
    public class OtherMode
    {
        /// <summary>
        /// 声明和类型模式
        /// </summary>
        public static void Method()
        {
            {
                object greeting = "Hello, World!";
                if (greeting is string message)
                {
                    Console.WriteLine(message.ToLower());  // output: hello, world!
                }
            }
            {
                //类型为 T 的声明模式在表达式结果为非 null 且满足以下任一条件时与表达式匹配
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

            {
                //表达式结果的运行时类型是具有基础类型 T 的可为 null 的值类型。
                //存在从表达式结果的运行时类型到类型 T 的装箱 或取消装箱转换
                int? xNullable = 7;
                int y = 23;
                object yBoxed = y;
                if (xNullable is int a && yBoxed is int b)
                {
                    Console.WriteLine(a + b);  // output: 30
                }
            }


           


        }
      
    }


    //如果只想检查表达式类型，可使用弃元 _ 代替变量名
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

        /// <summary>
        /// 可对此使用类型模式
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static decimal CalculateToll2(this Vehicle vehicle) => vehicle switch
        {
            Car => 2.00m,
            Truck => 7.50m,
            null => throw new ArgumentNullException(nameof(vehicle)),
            _ => throw new ArgumentException("Unknown type of a vehicle", nameof(vehicle)),
        };
    }
    //常量模式
    //关系模式
    //逻辑模式
    //属性模式
    //位置模式
    //var 模式
    //弃元模式
    //带括号模式
}
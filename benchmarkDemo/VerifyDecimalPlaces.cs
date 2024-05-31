using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace benchmarkDemo
{
    /// <summary>
    /// 验证小数点后几位
    /// </summary>
    public class VerifyDecimalPlaces
    {
        [Benchmark]
        public void Check1()
        {
            string decimalNumber = "3.14159";
            int decimalPlaces = Regex.Match(decimalNumber, @"\.\d+").Value.Length - 1;
            //Console.WriteLine(decimalPlaces); // 输出结果为5
        }

        /// <summary>
        /// 这个性能最高欸 ！！！
        /// </summary>
        [Benchmark]
        public void Check2()
        {
            double number = 3.14159;
            string decimalString = number.ToString();
            int decimalPlaces = decimalString.Length - decimalString.IndexOf('.') - 1;
            //Console.WriteLine(decimalPlaces); // 输出结果为5
        }

        [Benchmark]
        public void Check3()
        {
            double number = 3.14159;
            string decimalString = number.ToString();
            int decimalPlaces = (int)Math.Pow(10, decimalString.Length - decimalString.IndexOf('.') - 1);
           // Console.WriteLine(decimalPlaces); // 输出结果为5
        }

    }
}

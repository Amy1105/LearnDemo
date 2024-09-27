using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn
{
    /// <summary>
    /// 此代码示例演示了DateTime Kind、Now和UtcNow属性以及SpecificKind（）、ToLocalTime（）,以及ToUniversalTime（）方法
    /// </summary>
    public class DatetimeAttribute
    {
        public static void show()
        {
            //获取当前时刻的日期和时间，调整为本地时区。
            DateTime saveNow = DateTime.Now;
            
            //获取以协调世界时（UTC）表示的当前时刻的日期和时间。
            DateTime saveUtcNow = DateTime.UtcNow;
            DateTime myDt;

            // 显示以UTC和当地时间表示的当前时刻的值和Kind属性。

            DisplayNow("UtcNow: ..........", saveUtcNow);
            DisplayNow("Now: .............", saveNow);
            Console.WriteLine();

            // 将当前时刻的Kind属性更改为DateTimeKind。Utc并显示结果。

            myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
            Display("Utc: .............", myDt);

            //将当前时刻的Kind属性更改为DateTimeKind。本地并显示结果。

            myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Local);
            Display("Local: ...........", myDt);

            // 将当前时刻的Kind属性更改为DateTimeKind。未指定并显示结果。

            myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Unspecified);
            Display("Unspecified: .....", myDt);
        }


        /// <summary>
        /// 显示DateTime结构的值和Kind属性、转换为本地时间的DateTime结构和转换为世界时的DateTime结构。
        /// </summary>
        public static string datePatt = @"M/d/yyyy hh:mm:ss tt";
        public static void Display(string title, DateTime inputDt)
        {
            DateTime dispDt = inputDt;
            string dtString;

            // Display the original DateTime.

            dtString = dispDt.ToString(datePatt);
            Console.WriteLine("{0} {1}, Kind = {2}",
                              title, dtString, dispDt.Kind);

            // Convert inputDt to local time and display the result.
            // If inputDt.Kind is DateTimeKind.Utc, the conversion is performed.
            // If inputDt.Kind is DateTimeKind.Local, the conversion is not performed.
            // If inputDt.Kind is DateTimeKind.Unspecified, the conversion is
            // performed as if inputDt was universal time.

            dispDt = inputDt.ToLocalTime();
            dtString = dispDt.ToString(datePatt);
            Console.WriteLine("  ToLocalTime:     {0}, Kind = {1}",
                              dtString, dispDt.Kind);

            // Convert inputDt to universal time and display the result.
            // If inputDt.Kind is DateTimeKind.Utc, the conversion is not performed.
            // If inputDt.Kind is DateTimeKind.Local, the conversion is performed.
            // If inputDt.Kind is DateTimeKind.Unspecified, the conversion is
            // performed as if inputDt was local time.

            dispDt = inputDt.ToUniversalTime();
            dtString = dispDt.ToString(datePatt);
            Console.WriteLine("  ToUniversalTime: {0}, Kind = {1}",
                              dtString, dispDt.Kind);
            Console.WriteLine();
        }

     
        /// <summary>
        /// 展示值和kind属性
        /// </summary>
        /// <param name="title"></param>
        /// <param name="inputDt"></param>
        public static void DisplayNow(string title, DateTime inputDt)
        {
            string dtString = inputDt.ToString(datePatt);
            Console.WriteLine("{0} {1}, Kind = {2}",
                              title, dtString, inputDt.Kind);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static void TimeZoneExample()
        {
            // 获取所有已知时区的列表
            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

            // 选择一个时区
            TimeZoneInfo timeZone = timeZones[0]; // 使用第一个时区，例如"Dateline Standard Time"
            var frenceTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            // 获取当前时间
            DateTime now = DateTime.Now;
            DateTime timeInfrence = TimeZoneInfo.ConvertTime(now, frenceTime);

            // 转换到指定时区的时间
            DateTime timeInNewYork = TimeZoneInfo.ConvertTime(now, timeZone);

            Console.WriteLine($"Current time in {timeZone.DisplayName}: {now}");
            Console.WriteLine($"Time in {timeZone.DisplayName} is {timeInNewYork} in New York");
        }


        public static void show()
        {
            //获取当前时刻的日期和时间，调整为本地时区。
            DateTime saveLocalNow = DateTime.Now;
            DisplayNow("LOcalNow: .............", saveLocalNow);

            //获取以协调世界时（UTC）表示的当前时刻的日期和时间。
            DateTime saveUtcNow = DateTime.UtcNow;

            // 显示以UTC和当地时间表示的当前时刻的值和Kind属性。
            DisplayNow("UtcNow: ..........", saveUtcNow);
           
                      
            Console.WriteLine();

            // 将当前时刻的Kind属性更改为DateTimeKind。Utc并显示结果。
            DateTime myDt = DateTime.SpecifyKind(saveLocalNow, DateTimeKind.Utc);
            Display("Utc: .............", myDt);

            //将当前时刻的Kind属性更改为DateTimeKind。本地并显示结果。

            myDt = DateTime.SpecifyKind(saveLocalNow, DateTimeKind.Local);
            Display("Local: ...........", myDt);

            // 将当前时刻的Kind属性更改为DateTimeKind。未指定并显示结果。

            myDt = DateTime.SpecifyKind(saveLocalNow, DateTimeKind.Unspecified);
            Display("Unspecified: .....", myDt);
        }


        /// <summary>
        /// 显示DateTime结构的值和Kind属性、转换为本地时间的DateTime结构和转换为世界时的DateTime结构。
        /// </summary>
        public static string datePatt = @"M/d/yyyy hh:mm:ss tt";
        public static void Display(string title, DateTime inputDt)
        {
            DateTime dispDt = inputDt;

            DisplayNow(datePatt, dispDt);

            string dtString;

            // Display the original DateTime.

            dtString = dispDt.ToString(datePatt);
            Console.WriteLine("{0} {1}, Kind = {2}",
                              title, dtString, dispDt.Kind);

            // 将inputDt转换为本地时间并显示结果。
            // 如果输入Dt。Kind是DateTimeKind。Utc，执行转换。
            // 如果输入Dt。Kind是DateTimeKind。本地，不执行转换。
            // 如果输入Dt。Kind是DateTimeKind。未指定，转换为
            // 就像inputDt是世界时间一样执行。

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
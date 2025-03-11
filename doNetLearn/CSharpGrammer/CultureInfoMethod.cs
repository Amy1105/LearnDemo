using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    /// <summary>
    /// 多语言环境中，CultureInfo 对程序显示的影响
    /// </summary>
    public class CultureInfoMethod
    {

        /// <summary>
        /// windows 和 Linux 时区id不同
        /// </summary>
        public static void GetTimeZones()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();

            // 打印每个时区的显示名称
            foreach (var timeZone in timeZones)
            {
                Console.WriteLine(timeZone.Id);
            }
         
            {
                // 设置当前线程的文化信息为西班牙语（西班牙）
                CultureInfo spanishCulture = new CultureInfo("es-ES");
                Thread.CurrentThread.CurrentCulture = spanishCulture;
                Thread.CurrentThread.CurrentUICulture = spanishCulture;
                // 创建一个DateTime对象
                DateTime now = DateTime.Now;
                // 输出格式化后的DateTime对象
                Console.WriteLine(now.ToString()); // 使用当前线程文化信息进行格式化
            }
            Console.WriteLine("DateTime kind 属性");
            {
                DatetimeDemo.show();
            }
        }


        public void CultureInfoWidthDatetime()
        {
            DateTime? CalibrationDate = null;
            if (DateTime.TryParseExact("2019-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date))
            {
                CalibrationDate = date;
            }
            if (DateTime.TryParseExact("2019-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.AllowLeadingWhite, out DateTime date1))
            {
                CalibrationDate = date1;
            }
            if (DateTime.TryParseExact("2019-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out DateTime date2))
            {
                CalibrationDate = date2;
            }

            //不同文化环境下，不同时间显示格式，时间可以比较吗？值一样吗？
            var dateA = "2025-02-05";
            if (DateTime.TryParseExact(dateA, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.AdjustToUniversal, out DateTime datea))
            {
                if (DateTime.TryParseExact(dateA, "yyyy-MM-dd", new CultureInfo("fr-FR"), DateTimeStyles.AdjustToUniversal, out DateTime dateb))
                {
                    if (datea == dateb)
                    {
                        Console.WriteLine("same value");
                    }
                    else
                    {
                        Console.WriteLine("no same value");
                    }
                }
            }
        }
    }
}

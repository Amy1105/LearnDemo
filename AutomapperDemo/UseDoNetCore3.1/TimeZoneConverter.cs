using System;
using System.Collections.Generic;
using System.Text;

namespace UseDoNetCore3._1
{
    public class TimeZoneConverter
    {
        public static DateTime ConvertToLocalTime(DateTime utcTime, string timeZoneId)
        {
            // 获取指定时区的TimeZoneInfo
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // 将UTC时间转换为DateTimeOffset
            DateTimeOffset utcDateTimeOffset = utcTime.ToUniversalTime();

            // 转换为本地时间
            DateTimeOffset localDateTimeOffset = utcDateTimeOffset.ToOffset(timeZone.GetUtcOffset(utcDateTimeOffset.DateTime));

            // 返回本地时间的DateTime
            return localDateTimeOffset.DateTime;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp1
{
    public class mytimezon
    {



        public const string China = "China";
        public const string Chinses = "zh-CN";
        public const string USA = "USA";
        public const string English = "en-US";
        public const string France = "France";
        public const string Franch = "fr-FR";
        public const string Japan = "Japan";
        public const string Japanese = "ja-JP";

        public const string FRTeamGroup = "FR-TEST-TEAM";
        public const string FRTeamGroupName = "FR-TEST-TEAM";


        public const string FRWarehouseName = "C0-0-030";
        public const string FRWarehouseLocationName = "C0-0-030-1";


        private static Dictionary<string, MyTimezonInfo> timeZoneDic = new Dictionary<string, MyTimezonInfo>()
        {
            { China,new MyTimezonInfo(wName: "China Standard Time" ,lName:"Asia/Shanghai")},
            { USA, new MyTimezonInfo(wName: "Central European Standard Time",lName:"America/New_York")},
            { Japan, new MyTimezonInfo(wName:"Tokyo Standard Time",lName:"Asia/Tokyo")},
            { France, new MyTimezonInfo(wName:"Romance Standard Time",lName:"Europe/Paris")}
        };

        public static bool IsJpEnv()
        {
            var nation = GetNationConfig();
            if (mytimezon.Japan.Equals(nation, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static bool IsFREnv()
        {
            var nation = GetNationConfig();
            if (mytimezon.France.Equals(nation, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static string GetNationFlag()
        {
            string flag = string.Empty;
            var nation = GetNationConfig();
            if (mytimezon.France.Equals(nation, StringComparison.CurrentCultureIgnoreCase))
            {
                flag = "FR";
            }
            else if (mytimezon.USA.Equals(nation, StringComparison.CurrentCultureIgnoreCase))
            {
                flag = "US";
            }
            else if (mytimezon.Japan.Equals(nation, StringComparison.CurrentCultureIgnoreCase))
            {
                flag = "JP";
            }
            else
            {
                flag = "";
            }
            return flag;
        }


        public static string getSNo(string businessType, string TimeStringFromat)
        {
            return GetNationFlag() + businessType + GetLocalTime().ToString(TimeStringFromat);
        }

        public static string getSNo(string businessType)
        {
            return GetNationFlag() + businessType;
        }


        public static DateTime UtCToLocalTime(string nation)
        {
            string TimeZoneId = string.Empty;
            var utcTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);//配置kind属性，不会影响时间值

            Console.WriteLine(GetOSPlatform());

            if ("Linux".Equals(GetOSPlatform()))
            {
                // 自动将 Windows 时区 ID 转换为 IANA 时区 ID
                TimeZoneId =timeZoneDic[nation].LName; //ianaTimeZoneId
            }
            else if ("Windows".Equals(GetOSPlatform()))
            {
                TimeZoneId = timeZoneDic[nation].WName;
            }
            else
            {
                throw new Exception("OS Is Unknown ");
            }
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);           
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
        }

        /// <summary>
        /// 获取UTC，根据header中的语言标志，转当地时间
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime GetLocalTime()
        {
            var nation = GetNationConfig();
            if (timeZoneDic.ContainsKey(nation))
            {
                return UtCToLocalTime(nation);
            }
            throw new ArgumentOutOfRangeException($"have no lang to map timeone,lang:{nation}");
        }


        public static string GetNationConfig()
        {
            //if (string.IsNullOrEmpty(AppSetting.Country))
            //{
            //    throw new ArgumentOutOfRangeException($"AppSetting have no Language Setting");
            //}
            return "France";
        }

        public static string GetOSPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "Windows";
            }
            else
            {
                return "Unknown";
            }
        }


        /// <summary>
        /// 获取当前的utc时间
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime GetUTCNow()
        {
            return DateTime.UtcNow;
        }
    }

    public class MyTimezonInfo
    {
        public MyTimezonInfo(string wName, string lName)
        {
            WName = wName;
            LName = lName;
        }

        public string WName {  get; set; }
        public string LName { get; set; }
    }
}

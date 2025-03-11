using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UseDoNetCore3._1.Models;


namespace UseDoNetCore3._1
{
    internal class Program
    {    
        static void Main(string[] args)
        {
            // 获取所有系统支持的时区Id列表
            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

            // 选择一个时区Id，例如"Eastern Standard Time"
            string timeZoneId = "Eastern Standard Time";

            // 确保选定的时区Id在列表中存在
            if (timeZones.Any(tz => tz.Id == timeZoneId))
            {
                // 示例：转换2023年1月1日的UTC时间到"Eastern Standard Time"对应的本地时间
                DateTime utcTime = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime localTime = TimeZoneConverter.ConvertToLocalTime(utcTime, timeZoneId);

                Console.WriteLine($"UTC time: {utcTime}");
                Console.WriteLine($"Local time in {timeZoneId}: {localTime}");
            }
            else
            {
                Console.WriteLine($"Time zone ID '{timeZoneId}' not found.");
            }
            //IMapperConfigurationExpression cfg=new MapperConfigurationExpression();
            //cfg.CreateMap<ThingDto, Thing>().EqualityComparison((dto, entity) => dto.ID == entity.ID);
            //var map = new MapperConfiguration(cfg);
            //map.CompileMappings();

            //cfg.AddProfile<OrganizationProfile>();

            //var dtos = new List<ThingDto>
            //{
            //    new ThingDto { ID = 1, Title = "test0" },
            //    new ThingDto { ID = 2, Title = "test2" }
            //};

            //var items = new List<Thing>
            //{
            //    new Thing { ID = 1, Title = "test1" },
            //    new Thing { ID = 3, Title = "test3" },
            //};

            //mapper.Map(dtos, items.ToList()).Should().HaveElementAt(0, items.First());
        }
    }
}

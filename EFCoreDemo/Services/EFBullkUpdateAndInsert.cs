using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Services
{
    public class EFBullkUpdateAndInsert
    {               
        [Benchmark]
        public static Task AddAndUpdatesAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                course.Title = "Desc Update " + counter++;
            }
            courses.AddRange(Common.GetCourses(10000));
            context.Courses.UpdateRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public static Task AddAndUpdateWithBullkAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses.AsNoTracking().ToList();
            foreach (var course in courses)
            {
                course.Title = "Desc .BulkUpdate " + counter++;
            }
            courses.AddRange(Common.GetCourses(10000));
            var configUpdateBy = new BulkConfig
            {
                //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
                SetOutputIdentity = true,
                //如果“标识”列存在并且未添加到UpdateByProp中，则会自动排除该列
                UpdateByProperties = new List<string> { nameof(Course.Title) },
                //如果需要更改超过一半的columns，则可以使用Properties ToExclude。不允许同时设置这两个列表。
                //PropertiesToInclude = new List<string> { nameof(Item.Name), nameof(Item.Description) }, // "Name" in list not necessary since is in UpdateBy
            };
            return context.BulkInsertOrUpdateAsync(courses, configUpdateBy);
        }       
    }
}

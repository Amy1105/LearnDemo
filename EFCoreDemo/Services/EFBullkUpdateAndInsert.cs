using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCoreDemo.Services
{
    public class EFBullkUpdateAndInsert
    {
        private const int Count = 10000;

        SchoolContext context = null;
        [GlobalSetup]
        public async Task Setup()
        {
            var connection = new SqliteConnection("Data Source=../../../School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);

        }

        [Benchmark]
        public  Task AddAndUpdatesAsync(SchoolContext context)
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
        public  Task AddAndUpdateWithBullkAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                course.Title = "Desc .BulkUpdate " + counter++;
            }
            courses.AddRange(Common.GetCourses(10000));
            var lst = courses.Where(x => x.Credits == null).Count();
            Console.WriteLine(lst);
            var configUpdateBy = new BulkConfig
            {
                //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
                SetOutputIdentity = true,
                //用于指定自定义属性，我们希望通过该属性进行更新。
                //UpdateByProperties = new List<string> { nameof(Course.CourseID) },
                //执行插入/更新时，可以通过添加明确选择要影响的属性他们的名字输入到PropertiesToInclude中。
                PropertiesToInclude = new List<string> { nameof(Course.Title),nameof(Course.Credits) }, // "Name" in list not necessary since is in UpdateBy
            };
            return context.BulkInsertOrUpdateAsync(courses, configUpdateBy,a=> WriteProgress(a));
        }

        private static void WriteProgress(decimal percentage)
        {
            Debug.WriteLine(percentage);
        }
    }
}

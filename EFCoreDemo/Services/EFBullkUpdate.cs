using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Services
{
    public class EFBullkUpdate
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
        public  Task UpdatesAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses;
            foreach (var course in courses)
            {
                course.Title = "Desc Update " + counter++;
            }
            context.Courses.UpdateRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public  Task UpdateWithBullkAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses.AsNoTracking();
            foreach (var course in courses)
            {
                course.Title = "Desc .BulkUpdate " + counter++;
            }
            var configUpdateBy = new BulkConfig
            {
                //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
                SetOutputIdentity = true,
                //如果“标识”列存在并且未添加到UpdateByProp中，则会自动排除该列
                UpdateByProperties = new List<string> { nameof(Course.Title) },
                //如果需要更改超过一半的columns，则可以使用Properties ToExclude。不允许同时设置这两个列表。
                //PropertiesToInclude = new List<string> { nameof(Item.Name), nameof(Item.Description) }, // "Name" in list not necessary since is in UpdateBy
            };
            return context.BulkUpdateAsync(courses, configUpdateBy);
        }
    }
}

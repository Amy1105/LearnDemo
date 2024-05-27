using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCoreDemo.Services
{
    public class EFBullkBenchmarkUpdate
    {
        private const int Count = 10000;                    

        [Benchmark]
        public async Task UpdatesAsync()
        {
            SqliteConnection connection = null;

            SchoolContext context = null;

            connection = new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);
            int counter = 0;
            var courses = context.Courses.Take(Count).AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc Update " + counter.ToString();
            }         
            context.Courses.UpdateRange(courses);
            await context.SaveChangesAsync();
        }

        [Benchmark]
        public Task UpdateWithBullkAsync()
        {
            SqliteConnection connection = null;

            SchoolContext context = null;

            connection = new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);
            int counter = 0;
            var courses = context.Courses.Take(Count).AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc .Bulk Update " + counter.ToString();
            }
            var configUpdateBy = new BulkConfig
            {
                //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
                SetOutputIdentity = true,
                //在执行“插入/更新”操作时，可以通过将要影响的属性的名称添加到“要包含的属性”中来显式选择要影响的特性
                PropertiesToInclude = new List<string> { nameof(Course.Title) },
                //用于指定自定义属性，我们希望通过该属性进行更新。
                //UpdateByProperties = new List<string> { nameof(Course.CourseID) },                
            };
            return context.BulkUpdateAsync(courses, configUpdateBy);
        }
    }
}

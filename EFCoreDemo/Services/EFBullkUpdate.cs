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
        public  void UpdatesAsync(SchoolContext context,int n)
        {
            int counter = 0;
            var courses = context.Courses.Where(x => x.CourseID == n).AsNoTracking().ToList();
            foreach (var course in courses)
            {                
                counter++;
                course.Title = "Desc Update " + counter.ToString();
            }
            context.Courses.UpdateRange(courses);
            context.SaveChanges();
        }

        [Benchmark]
        public  Task UpdateWithBullkAsync(SchoolContext context,int n)
        {
            int counter = 0;
            var courses = context.Courses.Where(x => x.CourseID == n).AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc .BulkUpdate " +counter.ToString() ;
            }
            var configUpdateBy = new BulkConfig
            {
                //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
                SetOutputIdentity = true,
                //在执行“插入/更新”操作时，可以通过将要影响的属性的名称添加到“要包含的属性”中来显式选择要影响的特性
                PropertiesToInclude = new List<string> { nameof(Course.Title) },
                //用于指定自定义属性，我们希望通过该属性进行更新。
                UpdateByProperties = new List<string> { nameof(Course.CourseID) },              
                CalculateStats = true,
                //如果需要更改超过一半的columns，则可以使用Properties ToExclude。不允许同时设置这两个列表。
                //PropertiesToInclude = new List<string> { nameof(Item.Name), nameof(Item.Description) }, // "Name" in list not necessary since is in UpdateBy
            };           
            return context.BulkUpdateAsync(courses, configUpdateBy);
        }
    }
}

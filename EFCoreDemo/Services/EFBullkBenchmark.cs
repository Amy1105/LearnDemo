using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCoreDemo.Services
{
    [MemoryDiagnoser]
    public class EFBullkBenchmark
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


        #region  新增
        [Benchmark]
        public  Task AddConectTablesAsync()
        {
            context.Courses.AddRange(Common.GetCourses(Count));
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public  Task AddConectTablesWithBullkAsync()
        {
            context.BulkInsertAsync(Common.GetCourses(Count));
            return context.BulkSaveChangesAsync();
        }

        #endregion

        [Benchmark]
        public void UpdatesAsync()
        {
            int counter = 0;
            var courses = context.Courses.AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc Update " + counter.ToString();
            }
            context.Courses.UpdateRange(courses);
            context.SaveChanges();
        }

        [Benchmark]
        public Task UpdateWithBullkAsync()
        {
            int counter = 0;
            var courses = context.Courses.AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc .BulkUpdate " + counter.ToString();
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



        [Benchmark]
        public Task AddAndUpdatesAsync()
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
        public Task AddAndUpdateWithBullkAsync()
        {
            int counter = 0;
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                course.Title = "Desc .BulkUpdate " + counter++;
            }
            courses.AddRange(Common.GetCourses(10000));                    
            var configUpdateBy = new BulkConfig
            {
                //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
                SetOutputIdentity = true,
                //用于指定自定义属性，我们希望通过该属性进行更新。
                //UpdateByProperties = new List<string> { nameof(Course.CourseID) },
                //执行插入/更新时，可以通过添加明确选择要影响的属性他们的名字输入到PropertiesToInclude中。
                PropertiesToInclude = new List<string> { nameof(Course.Title), nameof(Course.Credits) }, // "Name" in list not necessary since is in UpdateBy
            };
            return context.BulkInsertOrUpdateAsync(courses, configUpdateBy, a => WriteProgress(a));
        }

        private static void WriteProgress(decimal percentage)
        {
            Debug.WriteLine(percentage);
        }

        [Benchmark]
        public Task DeletesAsync()
        {
            var courses = context.Courses.AsNoTracking().ToList();
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public Task DeleteWithBullkAsync()
        {
            var courses = context.Courses.AsNoTracking().ToList();
            var configUpdateBy = new BulkConfig
            {
                SetOutputIdentity = true,
            };
            return context.BulkDeleteAsync(courses, configUpdateBy);
        }
    }
}

using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using EFCoreDemo.Seed;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    public class BulkExecute
    {
        private const int Count = 10000;

        public static void InitDB(SchoolContext context)
        {
            //建库
            context.Database.EnsureCreated();
            //初始化数据
            DbInitializer.Initialize(context);
            Console.WriteLine("data init.");
        }

        public static Task AddConectTablesAsync(SchoolContext _context)
        {
            _context.Courses.AddRange(Common.GetCourses(Count));
            return _context.SaveChangesAsync();
        }
       
        public static Task AddConectTablesWithBullkAsync(SchoolContext _context)
        {
            _context.BulkInsertAsync(Common.GetCourses(Count));
            return _context.BulkSaveChangesAsync();
        }
 
        public static Task UpdatesAsync(SchoolContext context, int n)
        {
            int counter = 0;
            var courses = context.Courses.Where(x => x.CourseID == n).AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc Update " + counter.ToString();
            }
            context.Courses.UpdateRange(courses);
            return  context.SaveChangesAsync();
        }
       
        public static Task UpdateWithBullkAsync(SchoolContext context, int n)
        {
            int counter = 0;
            var courses = context.Courses.Where(x => x.CourseID == n).AsNoTracking().ToList();
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
       
        public  static Task AddAndUpdatesAsync(SchoolContext context)
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

       
        public static Task AddAndUpdateWithBullkAsync(SchoolContext context)
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
                PropertiesToInclude = new List<string> { nameof(Course.Title), nameof(Course.Credits) }, // "Name" in list not necessary since is in UpdateBy
            };
            return context.BulkInsertOrUpdateAsync(courses, configUpdateBy, a => WriteProgress(a));
        }

        private static void WriteProgress(decimal percentage)
        {
            Debug.WriteLine(percentage);
        }

        public static Task DeletesAsync(SchoolContext context)
        {
            var courses = context.Courses.AsNoTracking().ToList();
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

       
        public static Task DeleteWithBullkAsync(SchoolContext context)
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

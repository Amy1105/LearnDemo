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
                PreserveInsertOrder = true,//确保实体按照entities List中的顺序插入到Db中

                SetOutputIdentity = false,//Id值将更新为数据库中的新值

                //Sql Server上的BulkInsertOrUpdate对于那些将要更新的列，它必须与Id列匹配，
                //或者如果使用UpdateByProperties，则必须与其他唯一列匹配，在这种情况下，orderBy使用这些道具而不是Id，这是由于Sql MERGE的工作方式

                //在执行“插入/更新”操作时，可以通过将要影响的属性的名称添加到“要包含的属性”中来显式选择要影响的特性
                PropertiesToInclude = new List<string> { nameof(Course.Title) },

                //用于指定自定义属性，我们希望通过该属性进行更新。
                //UpdateByProperties = new List<string> { nameof(Course.CourseID) },

            };
            return context.BulkUpdateAsync(courses, configUpdateBy);
        }
       
        public  static Task AddAndUpdatesAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                course.Title = "Desc Add Update " + counter++;
            }
            courses.AddRange(Common.GetCourses(Count));
            context.Courses.UpdateRange(courses);
            return context.SaveChangesAsync();
        }

        /// <summary>
        /// 对于BulkInsertOrUpdate和IdentityId的SQLite组合，自动设置将无法正常工作，因为它不像SqlServer那样具有完整的MERGE功能。
        /// 相反，列表可以分为两个列表，并分别调用BulkInsert和BulkUpdate。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task AddAndUpdateWithBullkAsync(SchoolContext context)
        {
            int counter = 0;
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                course.Title = "Desc Add Update .BulkUpdate " + counter++;
            }
            courses.AddRange(Common.GetCourses(Count));         
            var configUpdateBy = new BulkConfig
            {
                PreserveInsertOrder = true,//确保实体按照entities List中的顺序插入到Db中

                SetOutputIdentity = false,//Id值将更新为数据库中的新值

                //Sql Server上的BulkInsertOrUpdate对于那些将要更新的列，它必须与Id列匹配，
                //或者如果使用UpdateByProperties，则必须与其他唯一列匹配，在这种情况下，orderBy使用这些道具而不是Id，这是由于Sql MERGE的工作方式

                //在执行“插入/更新”操作时，可以通过将要影响的属性的名称添加到“要包含的属性”中来显式选择要影响的特性
                PropertiesToInclude = new List<string> { nameof(Course.Title), nameof(Course.Credits) },

                //用于指定自定义属性，我们希望通过该属性进行更新。当在UpdateByProps中设置多个道具时，然后通过组合的列进行匹配，
                //比如基于这些列的唯一约束。在同时具有“标识”列的情况下使用UpdateByProperties要求排除Id属性
                UpdateByProperties = new List<string> { nameof(Course.Title), nameof(Course.Credits) },
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

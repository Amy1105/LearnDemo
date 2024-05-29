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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EFCoreDemo.Services
{
    public class BulkExecute
    {
        private readonly SchoolContext context;
        public BulkExecute(SchoolContext _context)
        {
            context = _context;
        }
        private const int Count = 10000;

        public void InitDB()
        {
           
                //建库
                context.Database.EnsureCreated();
                //初始化数据
                DbInitializer.Initialize(context);
                Console.WriteLine("data init.");
                   
        }


        #region 基本增删改查
        /// <summary>
        /// insert
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public async Task BulkInsertAsync()
        {
          
                Console.WriteLine($"before insert bulk 课程:{context.Courses.Count()}条");
                await context.BulkInsertAsync(Common.GetCourses(Count));
                Console.WriteLine($"after insert bulk 课程:{context.Courses.Count()}条");
                        
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="context"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public async Task BulkUpdateAsync()
        {
           
                Console.WriteLine($"before update bulk");
                var sample = context.Courses.AsNoTracking().First();
                Common.Print(context, sample.CourseID);
                int counter = 0;
                var courses = context.Courses.AsNoTracking().ToList();
                foreach (var course in courses)
                {
                    counter++;
                    course.Title = "Desc .BulkUpdate " + counter.ToString();
                }
                await context.BulkUpdateAsync(courses);
                Console.WriteLine($"after update bulk");
                Common.Print(context, sample.CourseID);
                           
        }
     
        /// <summary>
        /// read
        /// </summary>
        /// <param name="context"></param>
        public  async Task BulkReadAsync()
        {
            using (var context = Helper.GetContext())
            {
                var items = new List<Course>();
                var sample = context.Courses.Take(10).AsNoTracking();
                foreach (var course in sample)
                {
                    items.Add(new Course { CourseID = course.CourseID });
                }
                await context.BulkReadAsync(items);
                foreach (var cource in items)
                {
                    Console.WriteLine($"课程:{cource.CourseID},{cource.Title},{cource.Credits}");
                }
            }                
        }
       
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task BulkDeleteAsync()
        {
            using (var context = Helper.GetContext())
            {
                Console.WriteLine($"before delete bulk 课程:{context.Courses.Count()}条");
                var courses = context.Courses.AsNoTracking().ToList();
                await context.BulkDeleteAsync(courses);
                Console.WriteLine($"after delete bulk 课程:{context.Courses.Count()}条");
            }               
        }
        #endregion

        #region bulkConfig 属性

        /// <summary>
        /// 如果未设置，则将具有相同的BatchSize值,默认2000，如果非零，则指定在生成通知事件之前要处理的行数。
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public Task NotifyAfterAsync()
        {
            using (var context = Helper.GetContext())
            {
                var configUpdateBy = new BulkConfig
                {
                    PreserveInsertOrder = true,
                    SetOutputIdentity = true,
                    NotifyAfter = 1,
                };
                return context.BulkInsertAsync(Common.GetCourses(Count), configUpdateBy, a => WriteProgress(a));
            }
            //使用BatchSize就不通知了吗  to do
        }


        /// <summary>
        /// UpdateByProperties 支持唯一索引
        /// </summary>
        /// <param name="context"></param>
        public void UpdateByProperties()
        {
            using (var context = Helper.GetContext())
            {
                var fakhouri = new Instructor
                {
                    FirstMidName = "Fadi",
                    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06")
                };

                var entities = new List<Department>();
                for (int i = 1; i < 10; i++)
                {
                    var mathematics = new Department
                    {
                        Name = "Mathematics",
                        Budget = 100000,
                        StartDate = DateTime.Parse("2007-09-01"),
                    };
                    entities.Add(mathematics);
                }
                context.BulkRead(
                    entities,
                    new BulkConfig
                    {
                        UpdateByProperties = new List<string> { nameof(Department.Name) }
                    }
                );
                foreach (var entity in entities)
                {
                    Console.WriteLine($"DepartmentID:{entity.DepartmentID},name:{entity.Name}");
                }
            }               
        }


        /// <summary>
        /// PreserveInsertOrder 
        /// SetOutputIdentity
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public Task PreserveInsertOrder()
        {
            using (var context = Helper.GetContext())
            {
                var configUpdateBy = new BulkConfig
                {
                    PreserveInsertOrder = true,
                    SetOutputIdentity = true,
                };
                return context.BulkInsertAsync(Common.GetCourses(Count), configUpdateBy);
            }                
        }



        /// <summary>
        /// 允许指定Db中不必映射到实体的表的自定义名称。允许指定Db中不必映射到实体的表的自定义名称。
        /// </summary>
        /// <param name="context"></param>
        public void CustomDestinationTableName()
        {
            using (var context = Helper.GetContext())
            {
                var items = new List<Course>() { new Course { CourseID = 4041 }, new Course { CourseID = 4042 }, new Course { CourseID = 4043 }, new Course { CourseID = 4044 } };
                context.BulkRead(items, b => b.CustomDestinationTableName = nameof(Course));
                foreach (var cource in items)
                {
                    Console.WriteLine($"课程:{cource.CourseID},{cource.Title}");
                }
            }             
        }



        /// <summary>
        /// CalculateStats
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public async Task CalculateStats()
        {
            using (var context = Helper.GetContext())
            {
                Console.WriteLine($"before insert:{context.Courses.Count()}条");
                var configUpdateBy = new BulkConfig
                {
                    PreserveInsertOrder = true,
                    SetOutputIdentity = true,
                    CalculateStats = true,
                };
                await context.BulkInsertAsync(Common.GetCourses(Count), configUpdateBy);

                Console.WriteLine($"after insert:{context.Courses.Count()}条");
                Console.WriteLine("本次执行，新增了：" + configUpdateBy.StatsInfo?.StatsNumberInserted);
                Console.WriteLine("本次执行，修改了：" + configUpdateBy.StatsInfo?.StatsNumberInserted);
                Console.WriteLine("本次执行，删除了：" + configUpdateBy.StatsInfo?.StatsNumberInserted);
            }                
        }



        /// <summary>
        /// PropertiesToInclude 
        /// 在执行“插入/更新”操作时，可以通过将要影响的属性的名称添加到“要包含的属性”中来显式选择要影响的特性
        /// PropertiesToInclude 和 PropertiesToExclude 不能同时存在
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task PropertiesToInclude()
        {
            using (var context = Helper.GetContext())
            {
                Console.WriteLine($"before PropertiesToInclude");
                var sample = context.Courses.AsNoTracking().First();
                Common.Print(context, sample.CourseID);
                int counter = 0;
                var courses = context.Courses.AsNoTracking().ToList();
                foreach (var course in courses)
                {
                    counter++;
                    course.Title = "Desc .BulkUpdate " + counter.ToString();
                    course.Credits = 0;
                }
                var config = new BulkConfig
                {
                    PropertiesToInclude = new List<string> { nameof(Course.Title) }
                };
                await context.BulkUpdateAsync(courses, config);
                Console.WriteLine($"after PropertiesToInclude");
                Common.Print(context, sample.CourseID);
            }                
        }


        /// <summary>
        /// PropertiesToExclude 
        ///  执行“插入/更新”时，可以通过将一个或多个属性的名称添加到“PropertiesToExclude”中来排除这些属性。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task PropertiesToExclude()
        {
            using (var context = Helper.GetContext())
            {
                Console.WriteLine($"before PropertiesToExclude");
                var sample = context.Courses.AsNoTracking().First();
                Common.Print(context, sample.CourseID);
                int counter = 0;
                var courses = context.Courses.AsNoTracking().ToList();
                foreach (var course in courses)
                {
                    counter++;
                    course.Title = "PropertiesToExclude update " + counter.ToString();
                    course.Credits = 0;
                }
                var config = new BulkConfig
                {
                    PropertiesToExclude = new List<string> { nameof(Course.Credits) }
                };
                await context.BulkUpdateAsync(courses, config);
                Console.WriteLine($"after PropertiesToExclude");
                Common.Print(context, sample.CourseID);
            }                
        }      

        #endregion



        /// <summary>
        /// insert&update
        /// 对于BulkInsertOrUpdate和IdentityId的SQLite组合，自动设置将无法正常工作，因为它不像SqlServer那样具有完整的MERGE功能。
        /// 相反，列表可以分为两个列表，并分别调用BulkInsert和BulkUpdate。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task insertAndUpdate()
        {
            using (var context = Helper.GetContext())
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
        }


        public Task insertAndUpdateAndDelete()
        {
            using (var context = Helper.GetContext())
            {
                return Task.CompletedTask;
            }               
        }



        private void WriteProgress(decimal percentage)
        {           
            Console.WriteLine(percentage);
        }      
    }
}

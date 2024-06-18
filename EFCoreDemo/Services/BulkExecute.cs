using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using EFCoreDemo.Seed;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
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
            context.Database.EnsureDeleted();
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
        public  void BulkInsert()
        {
            Console.WriteLine($"before insert bulk 课程:{context.Courses.Count()}条");
            List<Course> Courses = new List<Course>();
            List<Instructor> subEntities = new List<Instructor>();
            foreach (var i in Enumerable.Range(1, Count))
            {
                //这门课下的3个老师
                List<Instructor> instructors = new List<Instructor>();
                foreach (var j in Enumerable.Range(1, 3))
                {
                    instructors.Add(new Instructor
                    {
                        FirstMidName = "bulkKim" + i.ToString() + "-" + j.ToString(),
                        LastName = "Abercrombie" + i.ToString() + "-" + j.ToString(),
                        HireDate = DateTime.Parse("1995-03-11"),
                    });
                }
                var emodel = new Course
                {
                    Title = "bulkLiterature" + i.ToString(),
                    Credits = 5,
                };
                emodel.Instructors = instructors;
                //这门课
                Courses.Add(emodel);
            }
            var bulkConfig = new BulkConfig() { SetOutputIdentity = true }; //从数据库中返回id
            context.BulkInsert(Courses, bulkConfig);
            foreach (var entity in Courses)
            {
                foreach (var subEntity in entity.Instructors!)
                {
                    subEntity.CourseID = entity.CourseID; // 设置外键
                }
                subEntities.AddRange(entity.Instructors);
            }
            bulkConfig.SetOutputIdentity = false;
            context.BulkInsert(subEntities, bulkConfig);

            Console.WriteLine($"after insert bulk 课程:{context.Courses.Count()}条");
            //查询
            var courses = context.Courses.Include(x => x.Instructors).Where(x => x.CourseID < 10000);//.Take(10);
            foreach (var cource in courses)
            {
                Console.WriteLine($"课程:{cource.CourseID},{cource.Title}");
                foreach (var instructor in cource.Instructors)
                {
                    Console.WriteLine($"----教师 :{instructor.ID}-{instructor.FullName}");
                }
            }
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
        public async Task BulkReadAsync()
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


        /// <summary>
        /// delete
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task BulkDeleteAsync()
        {
            Console.WriteLine($"before delete bulk 课程:{context.Courses.Count()}条");
            var courses = context.Courses.AsNoTracking().ToList();
            await context.BulkDeleteAsync(courses);
            Console.WriteLine($"after delete bulk 课程:{context.Courses.Count()}条");
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
            var configUpdateBy = new BulkConfig
            {
                PreserveInsertOrder = true,
                SetOutputIdentity = true,
                NotifyAfter = 1,
            };
            return context.BulkInsertAsync(Common.GetCourses(Count), configUpdateBy, a => WriteProgress(a));            
        }

        /// <summary>
        /// UpdateByProperties 支持唯一索引
        /// </summary>
        /// <param name="context"></param>
        public void UpdateByProperties()
        {
            var entities = new List<Course>();
            for (int i = 1; i < 10; i++)
            {
                var emodel = new Course
                {
                    Title = "bulkLiterature" + i,
                    Credits = 5,
                };
                entities.Add(emodel);
            }
            context.BulkInsert(entities);
            var dbEntities = context.Courses.AsNoTracking().ToList();
            foreach (var entity in dbEntities)
            {
                Console.WriteLine($"Course:{entity.CourseID},Title:{entity.Title}");
            }
            var updateEntities = new List<Course>();
            for (int i = 1; i < 6; i++)
            {
                var emodel = new Course
                {
                    Title = "bulkLiterature" + i,
                    Credits = 4,
                };
                updateEntities.Add(emodel);
            }
            context.BulkInsertOrUpdate(updateEntities,
                new BulkConfig
                {
                    UpdateByProperties = new List<string> { nameof(Course.Title) }
                }
            );
            var dbEntities2 = context.Courses.AsNoTracking().ToList();
            foreach (var entity in dbEntities2)
            {
                Console.WriteLine($"Course:{entity.CourseID},Title:{entity.Title}");
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
            var configUpdateBy = new BulkConfig
            {
                PreserveInsertOrder = true,
                SetOutputIdentity = true,
            };
            return context.BulkInsertAsync(Common.GetCourses(Count), configUpdateBy);
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


        /// <summary>
        /// PropertiesToExclude 
        ///  执行“插入/更新”时，可以通过将一个或多个属性的名称添加到“PropertiesToExclude”中来排除这些属性。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task PropertiesToExclude()
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

        #endregion



        /// <summary>
        /// insert&update
        /// 对于BulkInsertOrUpdate和IdentityId的SQLite组合，自动设置将无法正常工作，因为它不像SqlServer那样具有完整的MERGE功能。
        /// 相反，列表可以分为两个列表，并分别调用BulkInsert和BulkUpdate。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void InsertorUpdate()
        {
           var count = context.Courses.Count();
            List<Course> Courses = new List<Course>();          
            foreach (var i in Enumerable.Range(1, 10))
            {                
                var emodel = new Course
                {
                    Title = "bulkLiterature" + i.ToString(),
                    Credits = 5,
                };              
                //这门课
                Courses.Add(emodel);
            }
            var bulkConfig = new BulkConfig() { SetOutputIdentity = true }; //从数据库中返回id
            context.BulkInsert(Courses, bulkConfig);
            List<Course> newCourses = context.Courses.ToList();
            foreach (var c in newCourses)
            {
                c.Title = "update new bulkLiterature";
            }
            foreach (var i in Enumerable.Range(1, 10))
            {
                //这门课
                newCourses.Add(new Course
                {                   
                    Title = "add new bulkLiterature" + i.ToString(),
                    Credits = 4,
                });
            }          
            var configUpdateBy = new BulkConfig
            {
                SetOutputIdentity = true,
                CalculateStats = true,
            };
            context.BulkInsertOrUpdate(newCourses, configUpdateBy);
            Console.WriteLine($"after insert bulk 课程:{context.Courses.Count()}条");
            //查询
            var list = context.Courses.ToList();//.Take(10);
            foreach (var cource in list)
            {
                Console.WriteLine($"课程:{cource.CourseID},{cource.Title}");
                foreach (var instructor in cource.Instructors)
                {
                    Console.WriteLine($"----教师 :{instructor.ID}-{instructor.FullName}");
                }
            }
            Console.WriteLine(configUpdateBy.StatsInfo?.StatsNumberInserted);
            Console.WriteLine(configUpdateBy.StatsInfo?.StatsNumberUpdated);
            Console.WriteLine(configUpdateBy.StatsInfo?.StatsNumberDeleted);
        }


        public void InsertorUpdateorDelete()
        {
            var count = context.Courses.Count();
            List<Course> Courses = new List<Course>();           
            foreach (var i in Enumerable.Range(1, 10))
            {
                var emodel = new Course
                {
                    Title = "bulkLiterature" + i.ToString(),
                    Credits = i,
                };
                //这门课
                Courses.Add(emodel);
            }
            var bulkConfig = new BulkConfig() { SetOutputIdentity = true }; //从数据库中返回id
            context.BulkInsert(Courses, bulkConfig);
            List<Course> newCourses = context.Courses.ToList();
            foreach (var c in newCourses)
            {
                c.Title = "update new bulkLiterature";
            }
            foreach (var i in Enumerable.Range(1, 10))
            {
                //这门课
                newCourses.Add(new Course
                {
                    Title = "add new bulkLiterature" + i.ToString(),
                    Credits = 4,
                });
            }           
            var bulkConfigSoftDel = new BulkConfig();
            bulkConfigSoftDel.SetOutputIdentity = true;
            bulkConfigSoftDel.CalculateStats = true;
            bulkConfigSoftDel.SetSynchronizeSoftDelete<Course>(a => new Course { Credits = 1 }); // 它没有从数据库中删除，而是将Quantity更新为0（通常的用例是：IsDeleted为True）
            context.BulkInsertOrUpdateOrDelete(newCourses, bulkConfigSoftDel);
            Console.WriteLine($"after insert bulk 课程:{context.Courses.Count()}条");
            //查询
            var list = context.Courses.ToList();//.Take(10);
            foreach (var cource in list)
            {
                Console.WriteLine($"课程:{cource.CourseID},{cource.Title}");                
            }
            Console.WriteLine(bulkConfigSoftDel.StatsInfo?.StatsNumberInserted);
            Console.WriteLine(bulkConfigSoftDel.StatsInfo?.StatsNumberUpdated);
            Console.WriteLine(bulkConfigSoftDel.StatsInfo?.StatsNumberDeleted);          
        }

        private void WriteProgress(decimal percentage)
        {           
            Console.WriteLine(percentage);
        }      
    }
}

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace EFCoreDemo.Services
{
    [MemoryDiagnoser]
    public class EFBullkInsert
    {
        private const int Count = 10000;
      
        #region  新增
        [Benchmark]
        public static Task AddConectTablesAsync(SchoolContext context)
        {           
            context.Courses.AddRange(GetCourses(Count));
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public static Task AddConectTablesWithBullkAsync(SchoolContext context)
        {
            context.BulkInsertAsync(GetCourses(Count));
            return context.BulkSaveChangesAsync();
        }

        #endregion

        /// <summary>
        /// 构建课程实体（联表）
        /// </summary>
        /// <returns></returns>
        public static List<Course> GetCourses(int Count)
        {
            List<Course> Courses = new List<Course>();
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
                //这门课
                Courses.Add(new Course
                {
                    Title = "bulkLiterature"+i.ToString(),
                    Credits =i,
                    Instructors = instructors      
                });
            }
            return Courses;
        }



        //#region  update    

        //[Benchmark]
        //public static Task UpdatesAsync(SchoolContext context)
        //{
        //    int counter = 0;
        //    var courses = context.Courses;
        //    foreach (var course in courses)
        //    {
        //        course.Title = "Desc Update " + counter++;
        //    }
        //    context.Courses.UpdateRange(courses);
        //    return context.SaveChangesAsync();
        //}

        //[Benchmark]
        //public static Task UpdateWithBullkAsync(SchoolContext context)
        //{
        //    int counter = 0;
        //    var courses = context.Courses.AsNoTracking();
        //    foreach (var course in courses)
        //    {
        //        course.Title = "Desc .BulkUpdate " + counter++;
        //    }
        //    var configUpdateBy = new BulkConfig
        //    {
        //        //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
        //        SetOutputIdentity = true,
        //        //如果“标识”列存在并且未添加到UpdateByProp中，则会自动排除该列
        //        UpdateByProperties = new List<string> { nameof(Course.Title) },
        //        //如果需要更改超过一半的columns，则可以使用Properties ToExclude。不允许同时设置这两个列表。
        //        //PropertiesToInclude = new List<string> { nameof(Item.Name), nameof(Item.Description) }, // "Name" in list not necessary since is in UpdateBy
        //    };
        //    return context.BulkUpdateAsync(courses, configUpdateBy);
        //}


        //#endregion

        //#region  add update

        //[Benchmark]
        //public static Task AddAndUpdatesAsync(SchoolContext context)
        //{
        //    int counter = 0;
        //    var courses = context.Courses.ToList();
        //    foreach (var course in courses)
        //    {
        //        course.Title = "Desc Update " + counter++;
        //    }
        //    courses.AddRange(GetCourses(10000));
        //    context.Courses.UpdateRange(courses);
        //    return context.SaveChangesAsync();
        //}

        //[Benchmark]
        //public static Task AddAndUpdateWithBullkAsync(SchoolContext context)
        //{
        //    int counter = 0;
        //    var courses = context.Courses.AsNoTracking().ToList();
        //    foreach (var course in courses)
        //    {
        //        course.Title = "Desc .BulkUpdate " + counter++;
        //    }
        //    courses.AddRange(GetCourses(10000));
        //    var configUpdateBy = new BulkConfig
        //    {
        //        //当对多个相关表执行BulkInsert时很有用，以获取表的PK并将其设置为第二个的FK。
        //        SetOutputIdentity = true,
        //        //如果“标识”列存在并且未添加到UpdateByProp中，则会自动排除该列
        //        UpdateByProperties = new List<string> { nameof(Course.Title) },
        //        //如果需要更改超过一半的columns，则可以使用Properties ToExclude。不允许同时设置这两个列表。
        //        //PropertiesToInclude = new List<string> { nameof(Item.Name), nameof(Item.Description) }, // "Name" in list not necessary since is in UpdateBy
        //    };
        //    return context.BulkInsertOrUpdateAsync(courses, configUpdateBy);
        //}
        //#endregion

    }
}

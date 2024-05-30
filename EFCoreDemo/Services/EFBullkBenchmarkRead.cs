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
using System.Linq;

namespace EFCoreDemo.Services
{
    public class EFBullkBenchmarkRead
    {
        private const int Count = 10000;                    

        [Benchmark]
        public void ReadAsync()
        {
            using (var context = Helper.GetContext())
            {               
                var courses = context.Courses.Take(Count).AsNoTracking().ToList();
                var ids=courses.Select(g => g.CourseID).ToList();
                var res= context.Courses.Where(x=> ids.Contains(x.CourseID)).Take(100).ToList(); 
                foreach(var course in res)
                {
                    //Console.WriteLine($"课程:{course.CourseID},{course.Title}");
                }
            }               
        }

        [Benchmark]
        public void ReadBullkAsync()
        {
            using (var context = Helper.GetContext())
            {
                var courses = context.Courses.Take(Count).AsNoTracking().ToList();
                var ids = courses.Select(g => new Course() { CourseID=g.CourseID }).ToList();                              
                 context.BulkUpdateAsync(ids);
                var res = ids.Take(100).ToList();
                foreach (var course in res)
                {
                    //Console.WriteLine($"课程:{course.CourseID},{course.Title}");
                }
            }               
        }
    }
}

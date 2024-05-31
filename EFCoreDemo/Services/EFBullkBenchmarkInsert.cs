using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using EFCoreDemo.Seed;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EFCoreDemo.Services
{
    public class EFBullkBenchmarkInsert
    {       
        [Params(100000, 1000000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            using SchoolContext context = new SchoolContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }

        #region  新增
        [Benchmark]
        public Task AddConectTablesAsync()
        {
            using SchoolContext context = new SchoolContext();

            context.Courses.AddRange(Common.GetCourses(Count));
            return context.SaveChangesAsync();

        }

        [Benchmark]
        public Task AddConectTablesWithBullkAsync()
        {
            using SchoolContext context = new SchoolContext();
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
                    Instructors = instructors,
                };
                //这门课
                Courses.Add(emodel);
            }
            var bulkConfig = new BulkConfig() { SetOutputIdentity = true };
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
            return Task.CompletedTask;
        }

        #endregion
    }
}

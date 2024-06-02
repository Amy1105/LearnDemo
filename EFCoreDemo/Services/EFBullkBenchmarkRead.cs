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

        [GlobalSetup]
        public void Setup()
        {
            using SchoolContext context = new SchoolContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }

        [Benchmark]
        public void ReadAsync()
        {
            using SchoolContext context = new SchoolContext();
            var courses = context.Courses.Include(x=>x.Instructors).AsNoTracking().ToList();           
        }

        [Benchmark]
        public void ReadBullkAsync()
        {
            using SchoolContext context = new SchoolContext();
            var courses = context.Courses.Include(x => x.Instructors).AsNoTracking().ToList();
            var ids = courses.Select(g => new Course() { CourseID = g.CourseID }).ToList();
            context.BulkUpdateAsync(ids);         
        }
    }
}

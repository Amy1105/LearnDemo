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

namespace EFCoreDemo.Services
{
    public class EFBullkBenchmarkUpdate
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
        public async Task UpdatesAsync()
        {
            using SchoolContext context = new SchoolContext();
            int counter = 0;
            var courses = context.Courses.AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc Update " + counter.ToString();
            }
            context.Courses.UpdateRange(courses);
            await context.SaveChangesAsync();
        }

        [Benchmark]
        public Task UpdateWithBullkAsync()
        {
            using SchoolContext context = new SchoolContext();
            int counter = 0;
            var courses = context.Courses.AsNoTracking().ToList();
            foreach (var course in courses)
            {
                counter++;
                course.Title = "Desc .Bulk Update " + counter.ToString();
            }
            return context.BulkUpdateAsync(courses);
        }
    }
}

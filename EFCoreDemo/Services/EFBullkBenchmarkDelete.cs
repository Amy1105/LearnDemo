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
    public class EFBullkBenchmarkDelete
    {
        [GlobalSetup]
        public void Setup()
        {
            using SchoolContext context = new SchoolContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        [Benchmark]
        public Task DeletesAsync()
        {
            using SchoolContext context = new SchoolContext();
            var courses = context.Courses.AsNoTracking().ToList();
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public Task DeleteWithBullkAsync()
        {
            using SchoolContext context = new SchoolContext();
            var courses = context.Courses.AsNoTracking().ToList();
            return context.BulkDeleteAsync(courses);
        }
    }
}

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
        private const int Count = 10000;
        SqliteConnection connection = null;   
        
        SchoolContext context = null;

        [GlobalSetup]
        public  void Setup()
        {
            connection= new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);
        }
     
        [Benchmark]
        public Task DeletesAsync()
        {
            var courses = context.Courses.AsNoTracking().ToList();
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public Task DeleteWithBullkAsync()
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

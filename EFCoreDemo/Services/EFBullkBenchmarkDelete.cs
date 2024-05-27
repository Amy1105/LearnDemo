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

        [Benchmark]
        public Task DeletesAsync()
        {
            SqliteConnection connection = null;
            SchoolContext context = null;
            connection = new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);
            var courses = context.Courses.Take(Count).AsNoTracking().ToList();
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public Task DeleteWithBullkAsync()
        {
            SqliteConnection connection = null;
            SchoolContext context = null;
            connection = new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);
            var courses = context.Courses.Take(Count).AsNoTracking().ToList();           
            return context.BulkDeleteAsync(courses);
        }
    }
}

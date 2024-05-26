using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Services
{
    public class EFBullkDelete
    {

        private const int Count = 10000;

        SchoolContext context = null;
        [GlobalSetup]
        public async Task Setup()
        {
            var connection = new SqliteConnection("Data Source=../../../School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);

        }

        [Benchmark]
        public  Task DeletesAsync(SchoolContext context)
        {           
            var courses = context.Courses.AsNoTracking().ToList();           
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public  Task DeleteWithBullkAsync(SchoolContext context)
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

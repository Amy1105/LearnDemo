using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EFCore.BulkExtensions;
using EFCore.BulkExtensions.SqlAdapters;
using EFCoreDemo.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EFCoreDemo.Services
{
    [MemoryDiagnoser]
    public class EFBullkExcute
    {
        private const int Count = 10000;

        SchoolContext _context = null;
        [GlobalSetup]
        public async Task Setup()
        {
            var connection = new SqliteConnection("Data Source=../../../School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            _context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);

        }


        #region  新增
        [Benchmark]
        public  Task AddConectTablesAsync()
        {
            _context.Courses.AddRange(Common.GetCourses(Count));
            return _context.SaveChangesAsync();
        }

        [Benchmark]
        public  Task AddConectTablesWithBullkAsync()
        {
            _context.BulkInsertAsync(Common.GetCourses(Count));
            return _context.BulkSaveChangesAsync();
        }

        #endregion

       



      
    }
}

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
    public class EFBullkBenchmarkInsert
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
     


        #region  新增
        [Benchmark]
        public  Task AddConectTablesAsync()
        {           
            context.Courses.AddRange(Common.GetCourses(Count));
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public  Task AddConectTablesWithBullkAsync()
        {
            context.BulkInsertAsync(Common.GetCourses(Count));
            return context.BulkSaveChangesAsync();
        }

        #endregion
    }
}

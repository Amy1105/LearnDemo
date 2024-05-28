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
using System.Reflection;

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
            SqliteConnection connection = null;
            SchoolContext context = null;
            connection = new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);

            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("当前执行文件所在的相对路径: " + directory);

        }



        #region  新增
        [Benchmark]
        public Task AddConectTablesAsync()
        {
            context.Courses.AddRange(Common.GetCourses(Count));
            return context.SaveChangesAsync();
        }

        //[Benchmark]
        //public  Task AddConectTablesWithBullkAsync()
        //{
        //    return context.BulkInsertAsync(Common.GetCourses(Count));            
        //}

        #endregion
    }
}

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
        private const int Count = 100000;     

        [GlobalSetup]
        public  void Setup()
        {
                   
        }

        #region  新增
        [Benchmark]
        public Task AddConectTablesAsync()
        {
            using (var context = Helper.GetContext())
            {
                context.Courses.AddRange(Common.GetCourses(Count));
                return context.SaveChangesAsync();
            }                      
        }

        [Benchmark]
        public Task AddConectTablesWithBullkAsync()
        {
            using (var context = Helper.GetContext())
            {                
                return context.BulkInsertAsync(Common.GetCourses(Count));
            }               
        }

        #endregion
    }
}

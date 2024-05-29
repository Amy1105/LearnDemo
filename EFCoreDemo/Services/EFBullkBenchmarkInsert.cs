using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EFCoreDemo.Services
{
    public class EFBullkBenchmarkInsert
    {
        private const int Count = 100000;
        private readonly IServiceProvider serviceProvider;
        public EFBullkBenchmarkInsert(IServiceProvider _serviceProvider)
        {
            serviceProvider= _serviceProvider;
        }
        [GlobalSetup]
        public  void Setup()
        {
                   
        }

        #region  新增
        [Benchmark]
        public Task AddConectTablesAsync()
        {            
            using (var context = serviceProvider.GetRequiredService<SchoolContext>())
            {
                context.Courses.AddRange(Common.GetCourses(Count));
                return context.SaveChangesAsync();
            }                      
        }

        [Benchmark]
        public Task AddConectTablesWithBullkAsync()
        {
            using (var context = serviceProvider.GetRequiredService<SchoolContext>())
            {                
                return context.BulkInsertAsync(Common.GetCourses(Count));
            }               
        }

        #endregion
    }
}

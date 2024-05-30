using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using EFCoreDemo.Seed;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EFCoreDemo.Services
{
    public class EFBullkBenchmarkInsert
    {
        private const int Count = 100000;

        //private IServiceProvider serviceProvider=null;    
        
        [GlobalSetup]
        public  void Setup()
        {
            //var services = new ServiceCollection();            
            //services.AddDbContext<SchoolContext>();          
            //serviceProvider = services.BuildServiceProvider();
            //var context = serviceProvider.GetRequiredService<SchoolContext>();
            //var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            //var context=new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);           
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //DbInitializer.Initialize(context);

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

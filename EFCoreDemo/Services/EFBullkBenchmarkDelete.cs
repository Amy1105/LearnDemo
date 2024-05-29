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
        [Benchmark]
        public Task DeletesAsync()
        {
            using (var context = Helper.GetContext())
            {
                var courses = context.Courses.AsNoTracking().ToList();
                context.Courses.RemoveRange(courses);
                return context.SaveChangesAsync();
            }               
        }

        [Benchmark]
        public Task DeleteWithBullkAsync()
        {
            using (var context = Helper.GetContext())
            {
                var courses = context.Courses.AsNoTracking().ToList();
                return context.BulkDeleteAsync(courses);
            }                
        }
    }
}

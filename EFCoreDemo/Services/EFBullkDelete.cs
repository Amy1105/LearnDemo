using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Services
{
    public class EFBullkDelete
    {
        [Benchmark]
        public static Task DeletesAsync(SchoolContext context)
        {           
            var courses = context.Courses.AsNoTracking().ToList();           
            context.Courses.RemoveRange(courses);
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public static Task DeleteWithBullkAsync(SchoolContext context)
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

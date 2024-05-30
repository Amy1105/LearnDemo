using EFCoreDemo.Models;
using EFCoreDemo.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace EFCoreDemo
{
    public class SchoolContext : DbContext
    {        
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }


   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=../../../School.db");
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }       
    }
}
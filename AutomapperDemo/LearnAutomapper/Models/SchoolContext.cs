using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LearnAutomapper.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstructorDepartment> Departments { get; set; }
        public DbSet<CourseInstructor> Instructors { get; set; }   

        public DbSet<ex_Order> ex_Orders { get; set; }

        public DbSet<ex_OrderDetail> ex_OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=../../../School.db");
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SchoolAutomapper;ConnectRetryCount=0");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}

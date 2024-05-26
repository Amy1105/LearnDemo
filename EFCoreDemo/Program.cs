

using EFCoreDemo;
using EFCoreDemo.Models;
using EFCoreDemo.Seed;
using EFCoreDemo.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EFCore.BulkExtensions;
using BenchmarkDotNet.Running;

var connection = new SqliteConnection("Data Source=../../../School.db");
connection.Open();

var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
builder.UseSqlite(connection);
var context = new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);

//支持的环境
//支持的数据库类型


////建库
//context.Database.EnsureCreated();
////初始化数据
//DbInitializer.Initialize(context);
//Console.WriteLine("data init.");



//await EFBullkExcute.AddConectTablesAsync(context);
//await EFBullkExcute.AddConectTablesWithBullkAsync(context);

//查询
//Console.WriteLine($"insert课程:{context.Courses.Count()}条");
//var courses = context.Courses.Include(x => x.Instructors).Where(x=>x.CourseID>6000).Take(10);
//foreach (var cource in courses)
//{
//    Console.WriteLine($"课程:{cource.CourseID},{cource.Title}");

//    foreach (var instructor in cource.Instructors)
//    {
//        Console.WriteLine($"----教师 :{instructor.ID}-{instructor.FullName}");
//    }
//}

//新增基准测试
var summary = BenchmarkRunner.Run<EFBullkExcute>();


//update


//updateandinsert


//delete


Console.ReadKey();




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


//建库
//context.Database.EnsureCreated();
////加初始化数据
//DbInitializer.Initialize(context);
//Console.WriteLine("data init.");


var summery = BenchmarkRunnerCore.Run<typeof(Program).Assembly>();

//await EFBullkInsert.AddConectTablesAsync(context);


await EFBullkInsert.AddConectTablesWithBullkAsync(context);

//查询Course，Department，Enrollment，Instructor，OfficeAssignment，Student

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

//支持的环境
//支持的数据库类型


//EFBullkInsert.AddStudents(context);
//EFBullkInsert.AddStudentsWithBullk(context);


//事务

//var Count = context.Courses.Count();

//Console.WriteLine($"before  count:{Count}");
//Stopwatch stopwatch = Stopwatch.StartNew();

//await EFBullkInsert.AddConectTablesAsync(context);
//stopwatch.Stop();
//Console.WriteLine($"addRange  spend:{stopwatch.ElapsedMilliseconds} ms");
//Count = context.Courses.Count();
//Console.WriteLine($"after  count:{Count}");


//简单实体的批量添加



//复杂实体的批量添加和修改




//await EFBullkInsert.AddConectTablesWithBullkAsync(context);


//批量修改
/*
   context.Parents.Where(parent => parent.ParentId < 5 && !string.IsNullOrEmpty(parent.Details.Notes))
            .BatchUpdate(parent => new Parent { Description = parent.Details.Notes ?? "Fallback" });
 */



//批量删除

//联表 删除
/*
 context.Items.Include(x => x.ItemHistories).Where(x => !x.ItemHistories.Any()).OrderBy(x => x.ItemId).Skip(2).Take(4).BatchDelete();
*/

//支持原生sql


//B.修改：BulkUpdate，需要传入完整实体,不传的字段就会被更新为空   需要证实 to do ...



//bullk快速的原因？？？




{


    //LinqConect.SingleInclude(context);

   // LinqConect.SingleLINQ(context);

}
Console.ReadKey();


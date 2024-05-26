

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


{
    EFBullkExcute eFBullkExcute = new EFBullkExcute();

    //await eFBullkExcute.AddConectTablesAsync(context);
    //Console.WriteLine($"insert课程:{context.Courses.Count()}条");


    //await eFBullkExcute.AddConectTablesWithBullkAsync(context);
    //Console.WriteLine($"insert课程:{context.Courses.Count()}条");


    ////查询

    //var courses = context.Courses.Include(x => x.Instructors).Where(x => x.CourseID < 10000);//.Take(10);
    //foreach (var cource in courses)
    //{
    //    Console.WriteLine($"课程:{cource.CourseID},{cource.Title}");

    //    foreach (var instructor in cource.Instructors)
    //    {
    //        Console.WriteLine($"----教师 :{instructor.ID}-{instructor.FullName}");
    //    }
    //}

}
{
    EFBullkUpdate eFBullkUpdate = new EFBullkUpdate();
    //Console.WriteLine($"before update");
    //Common.Print(context, 7744);

    //eFBullkUpdate.UpdatesAsync(context,7744);

    //Console.WriteLine($"after update");
    //Common.Print(context, 7744);

    //Console.WriteLine($"before update");
    //Common.Print(context, 7745);
    //await eFBullkUpdate.UpdateWithBullkAsync(context, 7745);
    //Console.WriteLine($"after update");
    //Common.Print(context, 7745);
}


{
    EFBullkUpdateAndInsert eFBullkUpdateAndInsert = new EFBullkUpdateAndInsert();

    //Console.WriteLine($"before update");
    //Common.Print(context, 10500);  
    //await eFBullkUpdateAndInsert.AddAndUpdatesAsync(context);
    //Console.WriteLine($"after update");
    //Common.Print(context, 10500);

    //var c= context.Courses.Where(x => x.Credits > 5).Count();  

    //全部都成新增了，有问题  todo...     
    //Console.WriteLine($"before update");
    //Common.Print(context, 10501);
    //await eFBullkUpdateAndInsert.AddAndUpdateWithBullkAsync(context); 
    //Console.WriteLine($"after update");
    //Common.Print(context, 10501);

    //Console.WriteLine($"insert课程:{context.Courses.Count()}条");

}

{
    Console.WriteLine($"insert课程:{context.Courses.Count()}条");
    EFBullkDelete eFBullkDelete = new EFBullkDelete();
    
    //await eFBullkDelete.DeletesAsync(context);

    await eFBullkDelete.DeleteWithBullkAsync(context);
    Console.WriteLine($"insert课程:{context.Courses.Count()}条");
}
//新增基准测试
//var summary = BenchmarkRunner.Run<EFBullkExcute>();


//update


//updateandinsert


//delete


Console.ReadKey();


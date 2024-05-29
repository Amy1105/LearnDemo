using EFCoreDemo.Services;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using EFCoreDemo;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EFCoreDemo.Models;

//支持的环境
//支持的数据库类型

{      
    var bulkExecute = new BulkExecute();

    //bulkExecute.InitDB();

    //await bulkExecute.BulkInsertAsync();

    //await bulkExecute.BulkUpdateAsync();

    //await bulkExecute.BulkReadAsync();

    //await bulkExecute.BulkDeleteAsync();

    //属性

    //await bulkExecute.NotifyAfterAsync();

    //await bulkExecute.UpdateByProperties();  

    //await bulkExecute.CalculateStats();  ?

    //await bulkExecute.PropertiesToInclude();

    //await bulkExecute.PropertiesToExclude();  

    //任务：对接sqlserver，测试insertUpdate、insertUpdate、insertUpdateDelete方法，CalculateStats属性
    //to  do ...

    //查询
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


//基准测试
//var summary = BenchmarkRunner.Run<EFBullkBenchmarkInsert>();
//var summary = BenchmarkRunner.Run<EFBullkBenchmarkUpdate>();
//var summary = BenchmarkRunner.Run<EFBullkBenchmarkDelete>();
var summary = BenchmarkRunner.Run<EFBullkBenchmarkRead>();
Console.WriteLine("Done.");
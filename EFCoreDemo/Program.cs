using EFCoreDemo.Services;
using BenchmarkDotNet.Running;

//支持的环境
//支持的数据库类型

{
    //BulkExecute.InitDB(context);

    //Console.WriteLine($"before insert课程:{context.Courses.Count()}条");
    //await BulkExecute.AddConectTablesAsync(context);
    //Console.WriteLine($"after insert课程:{context.Courses.Count()}条");

    //Console.WriteLine($"before insert bulk 课程:{context.Courses.Count()}条");
    //await BulkExecute.AddConectTablesWithBullkAsync(context);
    //Console.WriteLine($"after insert bulk 课程:{context.Courses.Count()}条");

    //Console.WriteLine($"before update");
    //Common.Print(context, 7744);
    //await BulkExecute.UpdatesAsync(context, 7744);
    //Console.WriteLine($"after update");
    //Common.Print(context, 7744);

    //Console.WriteLine($"before update bulk");
    //Common.Print(context, 7745);
    //await BulkExecute.UpdateWithBullkAsync(context, 7745);
    //Console.WriteLine($"after update bulk");
    //Common.Print(context, 7745);

    //Console.WriteLine($"before add update");
    //Common.Print(context, 10500);
    //await BulkExecute.AddAndUpdatesAsync(context);
    //Console.WriteLine($"after add update");
    //Common.Print(context, 10500);
    //Console.WriteLine($"add update 课程:{context.Courses.Count()}条");

    //Console.WriteLine($"before add update bulk");
    //Common.Print(context, 8501);
    //await BulkExecute.AddAndUpdateWithBullkAsync(context);
    //Console.WriteLine($"after add update bulk");
    //Common.Print(context, 8501);
    //Console.WriteLine($"add update 课程:{context.Courses.Count()}条");

    //Console.WriteLine($"delete before课程:{context.Courses.Count()}条");
    //await BulkExecute.DeletesAsync(context);
    //Console.WriteLine($"delete after课程:{context.Courses.Count()}条");

    //Console.WriteLine($"delete bulk before课程:{context.Courses.Count()}条");
    //await BulkExecute.DeleteWithBullkAsync(context);
    //Console.WriteLine($"delete bulk after课程:{context.Courses.Count()}条");

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


//基准测试
var summary = BenchmarkRunner.Run<EFBullkBenchmarkInsert>();


Console.WriteLine("summary:");
Console.WriteLine(summary);

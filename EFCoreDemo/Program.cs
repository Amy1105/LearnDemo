using EFCoreDemo.Services;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Services.Example;


try
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Configuration.AddCommandLine(args);
    builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
    builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();

    builder.Configuration.AddJsonFile("appsetting.json", optional: true);


    var str = builder.Configuration.GetSection("ConnectionStrings")["SchoolDB"];

    builder.Services.AddDbContext<SchoolContext>();

    builder.Services.AddTransient<BulkExecute>();
    builder.Services.AddTransient<LinqConect>();    
    builder.Services.AddTransient<EFBullkBenchmarkInsert>();
    builder.Services.AddTransient<EFBullkBenchmarkUpdate>();
    builder.Services.AddTransient<EFBullkBenchmarkDelete>();
    builder.Services.AddTransient<EFBullkBenchmarkRead>();

    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var bulkExecute = services.GetRequiredService<BulkExecute>();
        bulkExecute.InitDB();

        //await bulkExecute.BulkInsertAsync();

         bulkExecute.insertorUpdateorDelete();

        //var linqConect = services.GetRequiredService<LinqConect>();
        //linqConect.SingleInclude();
        //linqConect.SingleLINQ();
        //linqConect.MultipleInclude();
        //linqConect.MultipleLINQ();
        //linqConect.SingleChildInclude();
        //linqConect.SingleChildLINQ();
        //linqConect.MultipleThenIncludes();
        //linqConect.IncludeTree();
        //linqConect.MultipleLeafIncludes();
        //linqConect.IncludeMultipleNavigationsWithSingleInclude();


        //linqConect.MultipleLeafIncludesFiltered2();
        //linqConect.LeftOuterJoin();
        //linqConect.LeftOuterJoinOrderBy();
        //linqConect.LeftOuterJoinWithLinQ();

        //await bulkExecute.BulkInsertAsync();        
        //await bulkExecute.BulkUpdateAsync();
        //await bulkExecute.BulkReadAsync();
        //await bulkExecute.BulkDeleteAsync();

        //属性
        // await bulkExecute.NotifyAfterAsync();
        // bulkExecute.UpdateByProperties();
        //// await bulkExecute.CalculateStats();  //?
        // await bulkExecute.PropertiesToInclude();
        // await bulkExecute.PropertiesToExclude();



        //任务：对接sqlserver，测试insertUpdate、insertUpdateDelete方法，CalculateStats属性
        //to  do ...


    }

    //基准测试
    //var sumeryInert = BenchmarkRunner.Run<EFBullkBenchmarkInsert>();
     //var sumeryUpdate = BenchmarkRunner.Run<EFBullkBenchmarkUpdate>();
    //var sumeryDelete=BenchmarkRunner.Run<EFBullkBenchmarkDelete>();
     //var sumeryRead=BenchmarkRunner.Run<EFBullkBenchmarkRead>();

    Console.WriteLine("Done.");    
    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}



    


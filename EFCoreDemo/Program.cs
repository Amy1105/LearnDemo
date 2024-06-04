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
    //var str = builder.Configuration.GetSection("ConnectionStrings")["SchoolDB"];
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
        //await bulkExecute.BulkInsertAsync();        
        //await bulkExecute.BulkUpdateAsync();
        //await bulkExecute.BulkReadAsync();
        //await bulkExecute.BulkDeleteAsync();

        //属性
        //await bulkExecute.NotifyAfterAsync();
        //bulkExecute.UpdateByProperties();       
        //await bulkExecute.PropertiesToInclude();
        //await bulkExecute.PropertiesToExclude();
        //bulkExecute.InsertorUpdate();
        //bulkExecute.InsertorUpdateorDelete();

        var linqConect = services.GetRequiredService<LinqConect>();
        linqConect.Check();
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
    }

    //基准测试
    //BenchmarkRunner.Run<EFBullkBenchmarkInsert>();
    //BenchmarkRunner.Run<EFBullkBenchmarkUpdate>();
    //BenchmarkRunner.Run<EFBullkBenchmarkDelete>();
    //BenchmarkRunner.Run<EFBullkBenchmarkRead>();

    Console.WriteLine("Done.");
    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}



    


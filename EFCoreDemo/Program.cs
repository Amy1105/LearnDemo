using EFCoreDemo.Services;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Services.Example;
using System.Reflection;
using AutoMapper;
using System.Text;


try
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Configuration.AddCommandLine(args);
    builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
    builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
    builder.Configuration.AddJsonFile("appsetting.json", optional: true);
    //var str = builder.Configuration.GetSection("ConnectionStrings")["SchoolDB"];
    builder.Services.AddDbContext<SchoolContext>();
    builder.Services.AddDbContext<TVCContext>();

    var assambly= Assembly.LoadFrom($"{AppDomain.CurrentDomain.BaseDirectory}\\EFCoreDemo.dll");
    builder.Services.AddAutoMapper(assambly);



    builder.Services.AddTransient<BulkExecute>();
    builder.Services.AddTransient<LinqConect>();
    builder.Services.AddTransient<SearchClass>();
    builder.Services.AddTransient<AttachMethod>();
    builder.Services.AddTransient<EFBullkBenchmarkInsert>();
    builder.Services.AddTransient<EFBullkBenchmarkUpdate>();
    builder.Services.AddTransient<EFBullkBenchmarkDelete>();
    builder.Services.AddTransient<EFBullkBenchmarkRead>();

    builder.Services.AddTransient<autoMapperDemo>();
    builder.Services.AddTransient<EnumService>();

    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var bulkExecute = services.GetRequiredService<BulkExecute>();
        //AttachMethod attachMethod = services.GetRequiredService<AttachMethod>();
        //autoMapperDemo attachMethodService = services.GetRequiredService<autoMapperDemo>();

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

        //var linqConect = services.GetRequiredService<LinqConect>();
        //linqConect.Check();
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

        //var searchExecute = services.GetRequiredService<SearchClass>();
        //searchExecute.GetTableInfoForMermaid();

        //attachMethod.Method2();

        //attachMethod.AddData();
        //attachMethod.Method1();

        //await  attachMethodService.Method1();
        //attachMethodService.Method4();
        //attachMethodService.Method3();

        //attachMethodService.Flattening();

        var enumService = services.GetRequiredService<EnumService>();
        enumService.InsertAddress();
        enumService.Search();

        string[] messages = [
            "Hello",
            "World",
            "from",
            "C#"
            ];
        var lines=string.Join("\n", messages);
         lines.Split("\n", StringSplitOptions.TrimEntries).Select(x=>x.Length);

       var s= lines.ReplaceLineEndings().Length;

        var message = "hello,world!";
        var array = message.ToCharArray();
        Array.Reverse(array);

        var res=new string(array);

       var s1= string.Join("", array);

        string sub = "\n";
        char c = '?';

        message.IndexOf(sub);
        message.CompareTo(c);

        /////////////////////////////////////////////////////

        var sb = new StringBuilder();
        sb.Append("Hello");
        sb.Replace("ll", "LL");
        sb.Remove(1, 3);
        sb.Insert(1, "ell");
        sb.Append(' ',5);
        sb.AppendLine("World!");
        sb.ToString();

        ///////////////////////////
        string.Join(Environment.NewLine, messages);//newline 通用平台的换行符

        //
        //Path.Combine  //跨平台的路径  斜杠

        //字符串池

    }

    //基准测试
    //BenchmarkRunner.Run<EFBullkBenchmarkInsert>();
    //BenchmarkRunner.Run<EFBullkBenchmarkUpdate>();
    //BenchmarkRunner.Run<EFBullkBenchmarkDelete>();
    //BenchmarkRunner.Run<EFBullkBenchmarkRead>();


    //to do...

    //.net cli 乱码

    //efcore 反向工程

    //efcore 可以读取表结构信息么

    //efcore 读操作

    //efcore 删改操作

    //efcore 增操作



    Console.WriteLine("Done.");
    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}



    


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchString;
using SearchString.ConcurrentFiles;
using SearchString.Models;




/*
1.ConcurrentBag:用于存储多线程处理的结果。ConcurrentBag 是线程安全的集合，适合在多线程环境中使用。

2.Parallel.ForEach:使用 Parallel.ForEach 并行处理文件读取和搜索操作，充分利用多核 CPU 的性能。

3.去重:使用 Distinct() 方法对结果去重。由于 ConcurrentBag 可能包含重复项（例如同一行被多次匹配），去重是必要的。

4.排序:使用 OrderBy 和 ThenBy 对结果按文件名和行号排序，使输出更有序。

5.异常处理:在文件读取过程中捕获异常，避免程序因单个文件读取失败而崩溃。
 * 
 */

// 指定要搜索的目录
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
//builder.Configuration.AddCommandLine(args);
//builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
//builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("appsetting.json", optional: true,reloadOnChange:true);

//appsetting.json 取不到值的原因，appsetting.json 属性需要设置成始终复制



ConnectionStringsOption connectionStringsOption = new();
builder.Configuration.GetSection(nameof(ConnectionStringsOption))
    .Bind(connectionStringsOption);



builder.Services.AddDbContext<myDBContext>(options =>
  options.UseSqlServer(connectionStringsOption.DbConnectionString));
builder.Services.AddTransient<dbService>();
builder.Services.AddTransient<MatchTest>();

builder.Services.AddTransient<SearchTextExecute>();

builder.Services.AddTransient<TestBlockingCollection>();
builder.Services.AddTransient<BlockingCollectionDemo>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    //多线程

    //MatchTest.GetVue();  //各种测试方法


    {
        SearchTextExecute searchTextExecute = scope.ServiceProvider.GetRequiredService<SearchTextExecute>();
        //string directoryPath = @"D:\Projects\LiMS";//  @"D:\Projects\LiMS\TVC.Server\TVC.ApplicationForm\Services\ApplicationForm\Partial";
        //await searchTextExecute.SearchText(directoryPath, "GLims", ".cs", ".js", ".vue");

        //查询帆软翻译键值
        string directoryPath = @"D:\rvc";
        await searchTextExecute.SearchText(directoryPath, "FanRuan", ".cpt");
    }


    //第二部分，与数据库中存在的翻译进行比较
    //读取excel的key，多线程查询
    {
        //myDBContext myDBContext = scope.ServiceProvider.GetRequiredService<myDBContext>();
        //var dbLists = myDBContext.sys_Text_Mains.Include(x => x.Sys_Texts).ToList();

        //var dbService = scope.ServiceProvider.GetRequiredService<dbService>();
        //await dbService.ExcelAnlysis(dbLists);

        //Console.WriteLine(@".\SearchResults" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx");
        ////Class1.PatternVue();

        //TestBlockingCollection.Method();
        //await BlockingCollectionDemo.Method();

    }

    Console.WriteLine("Done.");
}
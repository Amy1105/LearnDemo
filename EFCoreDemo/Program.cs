using EFCoreDemo.Services;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;
using EFCoreDemo.Seed;



try
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration.AddCommandLine(args);
        builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
        builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
        builder.Configuration.AddJsonFile("appsetting.json", optional: true);

        var str =builder.Configuration.GetSection("ConnectionStrings")["SchoolDB"];



        builder.Services.AddDbContext<SchoolContext>(
            option =>
            {
                option.UseSqlite(str);
            });
    
    builder.Services.AddTransient<BulkExecute>();
    builder.Services.AddTransient<EFBullkBenchmarkInsert>();
    builder.Services.AddTransient<EFBullkBenchmarkUpdate>();
    builder.Services.AddTransient<EFBullkBenchmarkDelete>();
    builder.Services.AddTransient<EFBullkBenchmarkRead>();
    //if (!builder.Environment.IsDevelopment())
    //{
    //    //builder.Services.add
    //}


    var app = builder.Build();

    //if (!app.Environment.IsDevelopment())
    //{
    //    app.UseExceptionHandler("/Error");
    //    app.UseHsts();
    //}
    //else
    //{
    //    app.UseDeveloperExceptionPage();
    //    app.UseMigrationsEndPoint();
    //}


    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var bulkExecute = services.GetRequiredService<BulkExecute>();
        //bulkExecute.InitDB();      

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

        //基准测试
        BenchmarkRunner.Run<EFBullkBenchmarkInsert>();
        //BenchmarkRunner.Run<EFBullkBenchmarkUpdate>();
        //BenchmarkRunner.Run<EFBullkBenchmarkDelete>();
        //BenchmarkRunner.Run<EFBullkBenchmarkRead>();
        Console.WriteLine("Done.");
    }       
    app.Run();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
    Console.ReadKey();


    



using LearnAutomapper;
using LearnAutomapper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

try
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Configuration.AddCommandLine(args);
    builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
    builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
    builder.Configuration.AddJsonFile("appsetting.json", optional: true);

    builder.Services.AddDbContext<SchoolContext>();


    var assambly = Assembly.LoadFrom($"{AppDomain.CurrentDomain.BaseDirectory}\\LearnAutomapper.dll");
    builder.Services.AddAutoMapper(assambly);



    builder.Services.AddTransient<CourseService>();


    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var courseService = services.GetRequiredService<CourseService>();
        //courseService.InitDB();
        await courseService.AddCourse();
        courseService.SelectCourse();
        //courseService.UpdateCourse()
    }
    Console.WriteLine("Done.");
    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}



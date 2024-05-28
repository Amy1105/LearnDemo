using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    });

await builder.Build().RunAsync();

var host = builder.Build();
using (host)
{
    var config = host.Services.GetRequiredService<IConfiguration>();
    // 使用config
    Console.WriteLine(config["ConnectionStrings"]);
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
    internal class Program
    {
        //static async Task Main(string[] args)
        //{            
        //    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        //    builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
        //    builder.Configuration.AddJsonFile("hostsettings.json", optional: true);
        //    builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
        //    builder.Configuration.AddCommandLine(args);

        //    using IHost host = builder.Build();

        //    // Application code should start here.

        //    await host.RunAsync();
        //}

        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
             builder.ConfigureAppConfiguration(a =>
            {
                a.AddJsonFile("hostsettings.json", optional: true);
                a.AddEnvironmentVariables(prefix: "PREFIX_");
                a.AddCommandLine(args);
                a.SetBasePath(Directory.GetCurrentDirectory());
            });
             //builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
            //builder.Configuration.AddJsonFile("hostsettings.json", optional: true);
            //builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
            //builder.Configuration.AddCommandLine(args);

            using IHost host = builder.Build();

            // Application code should start here.

            await host.RunAsync();
        }
    }
}

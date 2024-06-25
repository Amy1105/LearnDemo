using EFCoreDemo;
using EFCoreDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;

namespace TestProjectDemo
{
    public class UnitTest1
    {
        private readonly ServiceProvider serviceProvider;
        private readonly BulkExecute BulkExecuteService;
        public UnitTest1()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<SchoolContext>();
            serviceCollection.AddTransient<BulkExecute>();

             serviceProvider = serviceCollection.BuildServiceProvider();
             BulkExecuteService = serviceProvider.GetService<BulkExecute>();
        }


        [Fact]
        public void Test1()
        {
            BulkExecuteService.InitDB();
            Assert.Equal("", "");
        }
    }
}
// See https://aka.ms/new-console-template for more information
using Autofac;
using autofacDemo.Models;
using AutofacDemo.Models;
using Microsoft.Extensions.Logging;


//1.当生命周期范围被释放时，Autofac 可以为您处理这些组件的释放。


/**
 * 
但是，容器的存在时间与应用程序的生命周期相同。如果您直接从容器解析大量内容，则可能会有很多内容挂起等待处理。这可不是什么好事（您可能会看到“内存泄漏”）。
相反，从容器中创建一个子生命周期范围并从中解析。解析完组件后，处理子范围，一切都会为您清理干净。
 * 
 * 
 * 
 */



namespace autofacDemo;

/// <summary>
/// 1、注册组件
/// 2、解析服务
/// 3、控制范围和生命周期
/// 4、配置
/// 5、应用程序集成
/// 6、最佳实践和建议
/// 7、高级主题
/// 8、调试和故障排除
/// </summary>
public class Program
{
    private static IContainer Container { get; set; }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");


        var builder = new ContainerBuilder();
        builder.RegisterType<ConsoleOutput>().As<IOutput>();
        builder.RegisterType<TodayWriter>().As<IDateWriter>();
        Container = builder.Build();

        // The WriteDate method is where we'll make use
        // of our dependency injection. We'll define that
        // in a bit.
        WriteDate();


        //当使用基于反射的组件时，Autofac 会自动使用从容器中可获取的最多参数的类的构造函数。
        builder.RegisterType<ConsoleOutput>();
        builder.RegisterType(typeof(ConsoleOutput));


        builder.RegisterType<MyComponent>();
       //builder.RegisterType<ConsoleLogger>().As<ILogger>();
        var container = builder.Build();
        using (var scope = container.BeginLifetimeScope())
        {
            var component = scope.Resolve<MyComponent>();
        }

        //指定构造函数
        // builder.RegisterType<MyComponent>()
        //.UsingConstructor(typeof(ILogger), typeof(IConfigReader));

        //预先生成一个对象实例并将其添加到容器中以供已注册的组件使用
        //var output = new StringWriter();
        //builder.RegisterInstance(output).As<TextWriter>();


        //执行此操作时需要考虑的一点是，Autofac会自动处理已注册组件的处理，您可能希望自己控制生命周期，
        //而不是让 AutofacDispose为您调用对象
        var output = new StringWriter();
        builder.RegisterInstance(output)
               .As<TextWriter>()
               .ExternallyOwned();  
    }

    public static void WriteDate()
    {
        // Create the scope, resolve your IDateWriter,
        // use it, then dispose of the scope.
        using (var scope = Container.BeginLifetimeScope())
        {
            var writer = scope.Resolve<IDateWriter>();
            writer.WriteDate();
        }
    }
}
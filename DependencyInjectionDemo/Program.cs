//研究注入方式一个接口有多个实现类，如何注入，如何使用指定的实现类


using DependencyInjectionDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

//方式一： 适用于需要遍历所有实现或动态选择的情况
//builder.Services.AddTransient<IPhone, OppOPhone>();
//builder.Services.AddTransient<IPhone, HuaweiPhone>();
//builder.Services.AddTransient<IPhone, HonorPhone>();
//builder.Services.AddTransient<IPhone, XiaoMiPhone>();


//方式二：使用命名/键控依赖注入（Keyed Services，.NET 8+ 内置支持）
builder.Services.AddKeyedTransient<IPhone, OppOPhone>("default");
builder.Services.AddKeyedTransient<IPhone, HuaweiPhone>("secondary");
builder.Services.AddKeyedTransient<IPhone, HonorPhone>("third");
builder.Services.AddKeyedTransient<IPhone, XiaoMiPhone>("four");



//方式三：使用工厂模式（自定义选择逻辑），适用于运行时动态决定使用哪个实现的情况
//builder.Services.AddTransient<OppOPhone>();
//builder.Services.AddTransient<HuaweiPhone>();
//builder.Services.AddTransient<HonorPhone>();
//builder.Services.AddTransient<XiaoMiPhone>();

//伪代码实现如下
//builder.Services.AddTransient<IPhone>(sp =>
//{
//    // 根据条件返回不同的实现
//    if (/* 某些条件 */)
//        return sp.GetRequiredService<OppOPhone>();
//    else if (/* 其他条件 */)
//        return sp.GetRequiredService<HuaweiPhone>();
//    else if (/* 其他条件 */)
//        return sp.GetRequiredService<XiaoMiPhone>();
//    else
//        return sp.GetRequiredService<HonorPhone>();
//});


//方式四：使用策略模式（结合 IOptions 或配置） 
//与方式三差不多
//builder.Services.AddTransient<OppOPhone>();
//builder.Services.AddTransient<HuaweiPhone>();
//builder.Services.AddTransient<HonorPhone>();
//builder.Services.AddTransient<XiaoMiPhone>();

//builder.Services.AddTransient<IPhone>(sp =>
//{
//    var config = sp.GetRequiredService<IConfiguration>();
//    var connectionType = config["RabbitMQ:ConnectionType"];

//    return connectionType switch
//    {
//        "default" => sp.GetRequiredService<OppOPhone>(),
//        "secondary" => sp.GetRequiredService<HuaweiPhone>(),
//        "third" => sp.GetRequiredService<HonorPhone>(),
//        "four" => sp.GetRequiredService<XiaoMiPhone>(),
//        _ => throw new InvalidOperationException("Unknown connection type")
//    };
//});

//测试类
builder.Services.AddTransient<MyService2>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
   scope.ServiceProvider.GetRequiredService<MyService2>().GetPhoneName();

}

Console.WriteLine("Done");
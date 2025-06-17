using RabbitMQ.Client;
using RabbitMQDemo;
using RabbitMQDemo.Services;

var builder = WebApplication.CreateBuilder(args);

//在 ASP.NET Core 中，HttpContextAccessor 是一个服务，它允许你在非控制器类中访问当前 HTTP 请求的 HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.Configure(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSingleton<IMessageProducer, BopProducer>(); //消息队列生产者

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

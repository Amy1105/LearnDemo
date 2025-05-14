using RabbitMQ.Client;
using RabbitMQDemo;
using RabbitMQDemo.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IMessageProducer, BopProducer>(); //消息队列生产者

// 添加后台服务
builder.Services.AddHostedService<OrderCreatedConsumer>();
builder.Services.AddScoped<IOrderProcessor, OrderProcessor>(); //消息队列消费者


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

using RabbitMQ.Client;
using RabbitMQDemo;
using RabbitMQDemo.Services;

var builder = WebApplication.CreateBuilder(args);

//�� ASP.NET Core �У�HttpContextAccessor ��һ���������������ڷǿ��������з��ʵ�ǰ HTTP ����� HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.Configure(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSingleton<IMessageProducer, BopProducer>(); //��Ϣ����������

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

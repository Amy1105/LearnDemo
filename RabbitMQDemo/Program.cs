using RabbitMQ.Client;
using RabbitMQDemo;
using RabbitMQDemo.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IMessageProducer, BopProducer>(); //��Ϣ����������

// ��Ӻ�̨����
builder.Services.AddHostedService<OrderCreatedConsumer>();
builder.Services.AddScoped<IOrderProcessor, OrderProcessor>(); //��Ϣ����������


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

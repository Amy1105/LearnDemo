using RabbitMQ.Client;
using RabbitMQDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddHostedService<BopConsumerService>();

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

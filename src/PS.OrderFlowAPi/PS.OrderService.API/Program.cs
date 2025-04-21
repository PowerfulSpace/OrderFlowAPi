using Microsoft.EntityFrameworkCore;
using PS.OrderService.Application.Interfaces;
using PS.OrderService.Application.Services;
using PS.OrderService.Infrastructure.Messaging;
using PS.OrderService.Infrastructure.Persistence;
using PS.OrderService.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMessagePublisher, RabbitMqPublisher>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

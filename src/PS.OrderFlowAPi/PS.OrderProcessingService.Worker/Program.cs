using Microsoft.EntityFrameworkCore;
using PS.OrderProcessingService.Application.Interfaces;
using PS.OrderProcessingService.Infrastructure.Messaging;
using PS.OrderProcessingService.Infrastructure.Persistence;
using PS.OrderProcessingService.Infrastructure.Services;




var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IOrderProcessor, OrderProcessor>();
builder.Services.AddHostedService<RabbitMqConsumer>();



var host = builder.Build();
{
    await host.RunAsync();
}

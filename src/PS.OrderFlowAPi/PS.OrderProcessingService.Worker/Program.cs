using Microsoft.EntityFrameworkCore;
using PS.OrderProcessingService.Application.Interfaces;
using PS.OrderProcessingService.Infrastructure.Messaging;
using PS.OrderProcessingService.Infrastructure.Persistence;
using PS.OrderProcessingService.Infrastructure.Services;
using PS.OrderProcessingService.Worker;





var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IOrderProcessor, OrderProcessor>();
builder.Services.AddHostedService<RabbitMqConsumer>();
builder.Services.AddHostedService<Worker>();



var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IOrderProcessor, OrderProcessor>();
        services.AddHostedService<RabbitMqConsumer>();
    })
    .Build();

await host.RunAsync();

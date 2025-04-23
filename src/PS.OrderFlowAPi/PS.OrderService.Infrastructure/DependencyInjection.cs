using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.OrderService.Application.Interfaces;
using PS.OrderService.Application.Services;
using PS.OrderService.Infrastructure.Messaging;
using PS.OrderService.Infrastructure.Persistence;
using PS.OrderService.Infrastructure.Services;

namespace PS.OrderService.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
               .AddPersistance(configuration)
               .AddRabbitMq();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProcessor, OrderProcessor>();

            return services;
        }
        private static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
            return services;
        }


    }

}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PS.OrderProcessingService.Application.Interfaces;
using PS.OrderProcessingService.Domain.Entities;
using PS.OrderProcessingService.Domain.Enums;
using PS.OrderProcessingService.Infrastructure.Persistence;

namespace PS.OrderProcessingService.Infrastructure.Services
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OrderProcessor> _logger;

        public OrderProcessor(IServiceProvider serviceProvider, ILogger<OrderProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task ProcessOrderAsync(Order order)
        {

            using var scope = _serviceProvider.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();


            try
            {
                order.Status = OrderStatus.Processing;
                _dbContext.Orders.Add(order);

                order.Status = OrderStatus.Completed;
                _logger.LogInformation($"Processing order {order.Id} (Status: {order.Status})");
            }
            catch (Exception ex)
            {
                order.Status = OrderStatus.Failed;
                _logger.LogError(ex, $"Order {order.Id} processing failed");
            }
            finally
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

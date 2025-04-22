using Microsoft.Extensions.Logging;
using PS.OrderProcessingService.Application.Interfaces;
using PS.OrderProcessingService.Domain.Entities;
using PS.OrderProcessingService.Domain.Enums;
using PS.OrderProcessingService.Infrastructure.Persistence;

namespace PS.OrderProcessingService.Infrastructure.Services
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly OrderDbContext _dbContext;
        private readonly ILogger<OrderProcessor> _logger;

        public OrderProcessor(OrderDbContext dbContext, ILogger<OrderProcessor> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task ProcessOrderAsync(Order order)
        {
            _logger.LogInformation($"Processing order: {order.Id}");

            order.Status = OrderStatus.Processing;
            order.CreatedAt = DateTime.UtcNow;

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Order {order.Id} processed and saved.");
        }
    }
}

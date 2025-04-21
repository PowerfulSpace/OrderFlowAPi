using PS.OrderService.Application.DTOs;
using PS.OrderService.Application.Interfaces;
using PS.OrderService.Application.Services;
using PS.OrderService.Domain.Entities;

namespace PS.OrderService.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMessagePublisher _messagePublisher;

        public OrderService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                ProductName = dto.ProductName,
                Quantity = dto.Quantity
            };

            // Эмулируем сохранение и публикуем сообщение
            await _messagePublisher.PublishOrderCreatedAsync(order);

            return order.Id;
        }
    }
}

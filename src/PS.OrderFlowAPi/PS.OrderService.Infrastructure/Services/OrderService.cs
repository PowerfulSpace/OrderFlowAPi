using PS.OrderService.Application.DTOs;
using PS.OrderService.Application.Interfaces;
using PS.OrderService.Application.Services;
using PS.OrderService.Domain.Entities;
using PS.OrderService.Domain.Enums;

namespace PS.OrderService.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IMessagePublisher messagePublisher, IOrderRepository orderRepository)
        {
            _messagePublisher = messagePublisher;
            _orderRepository = orderRepository;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                Status = OrderStatus.Created, // Добавьте статус
                CreatedAt = DateTime.UtcNow
            };

            // Сначала сохраняем в БД
            await _orderRepository.AddAsync(order);

            // Эмулируем сохранение и публикуем сообщение
            await _messagePublisher.PublishOrderCreatedAsync(order);

            return order.Id;
        }
    }
}

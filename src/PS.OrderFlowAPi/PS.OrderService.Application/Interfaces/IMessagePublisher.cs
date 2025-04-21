using PS.OrderService.Domain.Entities;

namespace PS.OrderService.Application.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishOrderCreatedAsync(Order order);
    }
}

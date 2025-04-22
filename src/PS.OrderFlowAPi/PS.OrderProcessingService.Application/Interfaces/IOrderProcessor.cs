using PS.OrderProcessingService.Domain.Entities;

namespace PS.OrderProcessingService.Application.Interfaces
{
    public interface IOrderProcessor
    {
        Task ProcessOrderAsync(Order order);
    }
}

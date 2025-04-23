using PS.OrderService.Application.DTOs;

namespace PS.OrderService.Application.Services
{
    public interface IOrderProcessor
    {
        Task<Guid> CreateOrderAsync(CreateOrderDto dto);
    }
}

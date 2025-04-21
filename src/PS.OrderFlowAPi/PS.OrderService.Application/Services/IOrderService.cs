using PS.OrderService.Application.DTOs;

namespace PS.OrderService.Application.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderDto dto);
    }
}

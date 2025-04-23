using Microsoft.AspNetCore.Mvc;
using PS.OrderService.Application.DTOs;
using PS.OrderService.Application.Services;

namespace PS.OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProcessor _orderService;

        public OrdersController(IOrderProcessor orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var orderId = await _orderService.CreateOrderAsync(dto);
            return Ok(new { OrderId = orderId });
        }
    }
}

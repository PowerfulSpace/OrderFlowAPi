using Microsoft.AspNetCore.Mvc;
using PS.OrderService.Application.Services;

namespace PS.OrderService.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProcessor _orderService;

        public OrdersController(IOrderProcessor orderService)
        {
            _orderService = orderService;
        }
    }
}

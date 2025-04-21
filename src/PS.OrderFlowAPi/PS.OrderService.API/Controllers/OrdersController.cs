using Microsoft.AspNetCore.Mvc;

namespace PS.OrderService.API.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

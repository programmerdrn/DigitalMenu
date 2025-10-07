using Microsoft.AspNetCore.Mvc;
using DigitalMenu.Application.Interfaces;
using System.Threading.Tasks;

namespace DigitalMenu.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetPendingOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(System.Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
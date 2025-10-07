using Microsoft.AspNetCore.Mvc;
using DigitalMenu.Application.Interfaces;
using System.Threading.Tasks;
using System.Text.Json;
using DigitalMenu.Web.Models;

namespace DigitalMenu.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();

            // نمایش تعداد سبد خرید
            ViewBag.CartItemCount = GetCartItemCount();

            return View(products);
        }

        public async Task<IActionResult> Details(System.Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        private int GetCartItemCount()
        {
            try
            {
                var cartJson = HttpContext.Session.GetString("ShoppingCart");
                if (string.IsNullOrEmpty(cartJson))
                    return 0;

                var cart = JsonSerializer.Deserialize<ShoppingCart>(cartJson);
                return cart?.Items.Sum(item => item.Quantity) ?? 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
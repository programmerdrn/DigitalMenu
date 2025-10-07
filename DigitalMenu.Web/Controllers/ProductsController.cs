using DigitalMenu.Application.DTOs.ProductDTOs;
using DigitalMenu.Application.Interfaces;
using DigitalMenu.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

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
            try
            {
                // تست مستقیم بدون AutoMapper
                var products = await _productService.GetAllProductsAsync();

                // لاگ برای دیباگ
                Console.WriteLine($"Number of products: {products?.Count}");
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine($"Product: {product.Name}, Category: {product.CategoryName}");
                    }
                }

                return View(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View(new List<ProductDto>());
            }
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
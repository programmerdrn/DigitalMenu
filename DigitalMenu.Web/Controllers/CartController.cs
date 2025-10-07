using Microsoft.AspNetCore.Mvc;
using DigitalMenu.Application.Interfaces;
using DigitalMenu.Web.Models;
using System.Text.Json;

namespace DigitalMenu.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity = 1)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            var existingItem = cart.Items.FirstOrDefault(item => item.Product.Id == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            TempData["Success"] = $"{product.Name} به سبد خرید اضافه شد";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(Guid productId)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(item => item.Product.Id == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
                SaveCart(cart);
                TempData["Success"] = $"{item.Product.Name} از سبد خرید حذف شد";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int tableNumber)
        {
            var cart = GetCart();

            if (!cart.Items.Any())
            {
                TempData["Error"] = "سبد خرید خالی است";
                return RedirectToAction("Index");
            }

            // تبدیل سبد خرید به سفارش
            var orderItems = cart.Items.Select(item => new DigitalMenu.Application.DTOs.OrderDTOs.OrderItemDto
            {
                ProductId = item.Product.Id,
                Quantity = item.Quantity,
                UnitPrice = item.Product.Price
            }).ToList();

            try
            {
                var order = await _orderService.CreateOrderAsync(tableNumber, orderItems);

                // خالی کردن سبد خرید
                ClearCart();

                TempData["Success"] = $"سفارش شما با شماره {order.Id.ToString().Substring(0, 8)} ثبت شد";
                return RedirectToAction("Index", "Orders");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"خطا در ثبت سفارش: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            return Content(cart.Items.Sum(item => item.Quantity).ToString());
        }

        // تغییر سطح دسترسی از private به public
        public ShoppingCart GetCart()
        {
            var cartJson = HttpContext.Session.GetString("ShoppingCart");
            return cartJson == null ? new ShoppingCart() :
                JsonSerializer.Deserialize<ShoppingCart>(cartJson);
        }

        private void SaveCart(ShoppingCart cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("ShoppingCart", cartJson);
        }

        private void ClearCart()
        {
            HttpContext.Session.Remove("ShoppingCart");
        }

        // متد کمکی برای ProductsController
        public int GetCartItemCount()
        {
            var cart = GetCart();
            return cart.Items.Sum(item => item.Quantity);
        }
    }
}
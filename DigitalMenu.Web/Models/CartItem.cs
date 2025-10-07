using DigitalMenu.Application.DTOs.ProductDTOs;

namespace DigitalMenu.Web.Models
{
    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
    }

    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    }
}
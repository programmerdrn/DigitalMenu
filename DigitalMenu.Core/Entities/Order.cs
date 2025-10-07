using System;
using System.Collections.Generic;
using DigitalMenu.Core.Enums;

namespace DigitalMenu.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
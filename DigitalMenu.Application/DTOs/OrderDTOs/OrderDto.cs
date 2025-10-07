using System;
using System.Collections.Generic;
using DigitalMenu.Core.Enums;

namespace DigitalMenu.Application.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMenu.Application.DTOs.OrderDTOs;
using DigitalMenu.Core.Enums;

namespace DigitalMenu.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(int tableNumber, List<OrderItemDto> orderItems);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<List<OrderDto>> GetPendingOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
        Task<List<OrderDto>> GetOrdersByTableAsync(int tableNumber);
    }
}
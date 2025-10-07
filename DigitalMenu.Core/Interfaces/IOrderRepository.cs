using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMenu.Core.Entities;
using DigitalMenu.Core.Enums;

namespace DigitalMenu.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<Order> GetOrderWithItemsAsync(Guid orderId);
        Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableNumber);
    }
}
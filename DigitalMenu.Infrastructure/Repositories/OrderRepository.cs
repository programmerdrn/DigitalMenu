using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalMenu.Core.Entities;
using DigitalMenu.Core.Interfaces;
using DigitalMenu.Infrastructure.Data;
using DigitalMenu.Core.Enums;

namespace DigitalMenu.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DigitalMenuDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithItemsAsync(Guid orderId)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableNumber)
        {
            return await _dbSet
                .Where(o => o.TableNumber == tableNumber)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
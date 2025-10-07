using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalMenu.Core.Entities;
using DigitalMenu.Core.Interfaces;
using DigitalMenu.Infrastructure.Data;

namespace DigitalMenu.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DigitalMenuDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId && p.IsAvailable)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
        {
            return await _dbSet
                .Where(p => p.IsAvailable)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductWithCategoryAsync(Guid productId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        // این متد رو کاملاً ساده کردم - هیچ شرطی نداره
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DigitalMenuDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .Include(c => c.Products.Where(p => p.IsAvailable))
                .ToListAsync();
        }

        public async Task<Category> GetCategoryWithProductsAsync(Guid categoryId)
        {
            return await _dbSet
                .Include(c => c.Products.Where(p => p.IsAvailable))
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.IsActive);
        }
    }
}
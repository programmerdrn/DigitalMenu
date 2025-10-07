using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMenu.Core.Entities;

namespace DigitalMenu.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
        Task<Category> GetCategoryWithProductsAsync(Guid categoryId);
    }
}
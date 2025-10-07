using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMenu.Core.Entities;

namespace DigitalMenu.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Product>> GetAvailableProductsAsync();
        Task<Product> GetProductWithCategoryAsync(Guid productId);
    }
}
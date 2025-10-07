using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMenu.Application.DTOs.ProductDTOs;

namespace DigitalMenu.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<List<ProductDto>> GetProductsByCategoryAsync(Guid categoryId);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
        Task<ProductDto> UpdateProductAsync(Guid id, CreateProductDto productDto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<List<ProductDto>> GetAvailableProductsAsync();
    }
}
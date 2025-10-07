using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMenu.Application.DTOs.ProductDTOs;
using DigitalMenu.Application.Interfaces;
using DigitalMenu.Core.Entities;
using DigitalMenu.Core.Interfaces;

namespace DigitalMenu.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductWithCategoryAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return await GetProductByIdAsync(product.Id);
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, CreateProductDto productDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return null;

            _mapper.Map(productDto, existingProduct);
            _productRepository.Update(existingProduct);
            await _productRepository.SaveChangesAsync();

            return await GetProductByIdAsync(id);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            _productRepository.Delete(product);
            return await _productRepository.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAvailableProductsAsync()
        {
            var products = await _productRepository.GetAvailableProductsAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
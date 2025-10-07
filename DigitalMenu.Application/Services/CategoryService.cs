using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMenu.Application.DTOs.CategoryDTOs;
using DigitalMenu.Application.Interfaces;
using DigitalMenu.Core.Entities;
using DigitalMenu.Core.Interfaces;

namespace DigitalMenu.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesWithProductsAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(string name, string description)
        {
            var category = new Category
            {
                Name = name,
                Description = description
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return await GetCategoryByIdAsync(category.Id);
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            _categoryRepository.Delete(category);
            return await _categoryRepository.SaveChangesAsync();
        }
    }
}
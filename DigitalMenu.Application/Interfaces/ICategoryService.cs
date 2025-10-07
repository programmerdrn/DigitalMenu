using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMenu.Application.DTOs.CategoryDTOs;

namespace DigitalMenu.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> CreateCategoryAsync(string name, string description);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
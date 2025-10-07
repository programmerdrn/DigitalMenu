using System;
using System.Collections.Generic;
using DigitalMenu.Application.DTOs.ProductDTOs;

namespace DigitalMenu.Application.DTOs.CategoryDTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; } // این خط رو اضافه کن
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
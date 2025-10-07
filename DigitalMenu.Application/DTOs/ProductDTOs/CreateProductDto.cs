using System;

namespace DigitalMenu.Application.DTOs.ProductDTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int PreparationTime { get; set; }
        public Guid CategoryId { get; set; }
    }
}
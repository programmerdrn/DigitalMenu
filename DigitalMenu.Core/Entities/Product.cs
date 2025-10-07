using System;

namespace DigitalMenu.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int PreparationTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Category Category { get; set; }
    }
}
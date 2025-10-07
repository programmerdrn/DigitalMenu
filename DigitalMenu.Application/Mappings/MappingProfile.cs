using AutoMapper;
using DigitalMenu.Application.DTOs.CategoryDTOs;
using DigitalMenu.Application.DTOs.OrderDTOs;
using DigitalMenu.Application.DTOs.ProductDTOs;
using DigitalMenu.Core.Entities;

namespace DigitalMenu.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl ?? ""));

            CreateMap<CreateProductDto, Product>();

            // Category Mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon ?? "📁"));

            // Order Mappings
            CreateMap<Order, OrderDto>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
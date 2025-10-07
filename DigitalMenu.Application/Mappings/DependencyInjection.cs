using Microsoft.Extensions.DependencyInjection;
using DigitalMenu.Application.Interfaces;
using DigitalMenu.Application.Services;
using DigitalMenu.Application.Mappings;

namespace DigitalMenu.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
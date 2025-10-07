using Microsoft.AspNetCore.Mvc;
using DigitalMenu.Application.Interfaces;
using System.Threading.Tasks;

namespace DigitalMenu.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public MenuController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Category(System.Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
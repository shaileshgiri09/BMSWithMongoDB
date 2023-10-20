using BMS.Domain.Model;
using BMS.Infrastructure.IService;
using BMSWithMongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMSWithMongoDB.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories =  await _categoryService.GetAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Category category = await _categoryService.GetAsync(id);
            if(category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            Category category = new Category
            {
                Name = categoryDTO.Name
            };
            await _categoryService.CreateCategory(category);

            return CreatedAtAction(nameof(Get), new { id = category.Id }, categoryDTO);
        }
    }
}

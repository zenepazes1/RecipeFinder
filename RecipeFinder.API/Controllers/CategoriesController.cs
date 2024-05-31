using Microsoft.AspNetCore.Mvc;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RecipeFinder.API.Contracts;

namespace RecipeFinder.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var response = categories.Select(c => new CategoryResponse(c.CategoryId, c.Name));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            var response = new CategoryResponse(category.CategoryId, category.Name);
            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = new Category { Name = request.Name };
            var createdCategory = await _categoryService.CreateCategoryAsync(category);
            if (createdCategory == null)
            {
                return BadRequest("Category already exists.");
            }

            var response = new CategoryResponse(createdCategory.CategoryId, createdCategory.Name);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, response);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound($"Category with ID {id} not found.");

            existingCategory.Name = request.Name;
            await _categoryService.UpdateCategoryAsync(existingCategory);
            return NoContent(); // Standard response for a successful PUT request
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent(); // Standard response for a successful DELETE request
        }
    }
}

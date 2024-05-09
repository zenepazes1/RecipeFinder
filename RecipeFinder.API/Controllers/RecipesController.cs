using Microsoft.AspNetCore.Mvc;
using RecipeFinder.API.Contracts;
using RecipeFinder.Application.Service;
using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipesService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeResponse>>> GetAllRecipes()
        {
            var recipes = await _recipesService.GetAllRecipesAsync();
            var response = recipes.Select(r => new RecipeResponse(
                r.RecipeId,
                r.Title,
                r.Description,
                r.Instructions,
                r.PreparationTime,
                r.Difficulty,
                r.AuthorId,
                r.CategoryId,
                r.ImageUrl)).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeResponse>> CreateRecipe([FromBody] RecipeRequest request)
        {
            var recipe = new Recipe
            {
                Title = request.Title,
                Description = request.Description,
                Instructions = request.Instructions,
                PreparationTime = request.PreparationTime,
                Difficulty = request.Difficulty,
                AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl
            };
            var createdRecipe = await _recipesService.CreateRecipeAsync(recipe);
            return CreatedAtAction(nameof(GetRecipe), new { id = createdRecipe.RecipeId }, new RecipeResponse(
                createdRecipe.RecipeId,
                createdRecipe.Title,
                createdRecipe.Description,
                createdRecipe.Instructions,
                createdRecipe.PreparationTime,
                createdRecipe.Difficulty,
                createdRecipe.AuthorId,
                createdRecipe.CategoryId,
                createdRecipe.ImageUrl));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeRequest request)
        {
            var existingRecipe = await _recipesService.GetRecipeByIdAsync(id);
            if (existingRecipe == null)
            {
                return NotFound($"Recipe with ID {id} not found.");
            }

            existingRecipe.Title = request.Title;
            existingRecipe.Description = request.Description;
            existingRecipe.Instructions = request.Instructions;
            existingRecipe.PreparationTime = request.PreparationTime;
            existingRecipe.Difficulty = request.Difficulty;
            existingRecipe.AuthorId = request.AuthorId;
            existingRecipe.CategoryId = request.CategoryId;
            existingRecipe.ImageUrl = request.ImageUrl;

            await _recipesService.UpdateRecipeAsync(existingRecipe);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeResponse>> GetRecipe(int id)
        {
            var recipe = await _recipesService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            var response = new RecipeResponse(
                recipe.RecipeId,
                recipe.Title,
                recipe.Description,
                recipe.Instructions,
                recipe.PreparationTime,
                recipe.Difficulty,
                recipe.AuthorId,
                recipe.CategoryId,
                recipe.ImageUrl);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _recipesService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            await _recipesService.DeleteRecipeAsync(id);
            return NoContent();
        }

    }
}

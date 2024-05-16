using Microsoft.AspNetCore.Mvc;
using RecipeFinder.API.Contracts;
using RecipeFinder.Application.Services;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            var response = recipes.Select(r => new RecipeResponse(
                r.RecipeId,
                r.Title,
                r.Description,
                r.Instructions,
                r.PreparationTime,
                r.Difficulty,
                r.AuthorId,
                r.CategoryId,
                r.ImageUrl
            ));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
                return NotFound($"Recipe with ID {id} not found.");

            var response = new RecipeResponse(
                recipe.RecipeId,
                recipe.Title,
                recipe.Description,
                recipe.Instructions,
                recipe.PreparationTime,
                recipe.Difficulty,
                recipe.AuthorId,
                recipe.CategoryId,
                recipe.ImageUrl
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newRecipe = new Recipe
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

            var createdRecipe = await _recipeService.CreateRecipeAsync(newRecipe);
            if (createdRecipe == null)
                return BadRequest("Failed to create the recipe.");

            return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.RecipeId }, new RecipeResponse(
                createdRecipe.RecipeId,
                createdRecipe.Title,
                createdRecipe.Description,
                createdRecipe.Instructions,
                createdRecipe.PreparationTime,
                createdRecipe.Difficulty,
                createdRecipe.AuthorId,
                createdRecipe.CategoryId,
                createdRecipe.ImageUrl
            ));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRecipe = await _recipeService.GetRecipeByIdAsync(id);
            if (existingRecipe == null)
                return NotFound($"Recipe with ID {id} not found.");

            existingRecipe.Title = request.Title;
            existingRecipe.Description = request.Description;
            existingRecipe.Instructions = request.Instructions;
            existingRecipe.PreparationTime = request.PreparationTime;
            existingRecipe.Difficulty = request.Difficulty;
            existingRecipe.AuthorId = request.AuthorId;
            existingRecipe.CategoryId = request.CategoryId;
            existingRecipe.ImageUrl = request.ImageUrl;

            await _recipeService.UpdateRecipeAsync(existingRecipe);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
                return NotFound($"Recipe with ID {id} not found.");

            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRecipes(string searchTerm)
        {
            var recipes = await _recipeService.SearchRecipesAsync(searchTerm);
            var response = recipes.Select(r => new RecipeResponse(
                r.RecipeId,
                r.Title,
                r.Description,
                r.Instructions,
                r.PreparationTime,
                r.Difficulty,
                r.AuthorId,
                r.CategoryId,
                r.ImageUrl
            ));
            return Ok(response);
        }

    }
}

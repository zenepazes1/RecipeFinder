using Microsoft.AspNetCore.Mvc;
using RecipeFinder.API.Contracts;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RecipeFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        private readonly IFavoriteRecipeService _favoriteRecipeService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public RecipesController(IRecipeService recipeService, IIngredientService ingredientService, IFavoriteRecipeService favoriteRecipeService, ICategoryService categoryService, IUserService userService)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
            _favoriteRecipeService = favoriteRecipeService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            var recipeResponses = new List<RecipeResponse>();
            foreach (var recipe in recipes)
            {
                var recipeCategory = await _categoryService.GetCategoryByIdAsync(recipe.CategoryId);
                var recipeAuthor = await _userService.GetUserByIdAsync(recipe.AuthorId);
                recipe.Category = recipeCategory;
                recipe.Author = recipeAuthor;

                var ingredientResponses = recipe.Ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name)).ToList();
                var recipeResponse = new RecipeResponse(
                    recipe.RecipeId,
                    recipe.Title,
                    recipe.Description,
                    recipe.Instructions,
                    recipe.PreparationTime,
                    recipe.Difficulty,
                    recipe.ImageUrl,
                    recipe.AuthorId,
                    new UserResponse(recipeAuthor.Id, recipeAuthor.Email, recipeAuthor.FirstName, recipeAuthor.LastName),
                    recipe.CategoryId,
                    new CategoryResponse(recipeCategory.CategoryId, recipeCategory.Name),
                    ingredientResponses
                );
                recipeResponses.Add(recipeResponse);
            }
            return Ok(recipeResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
                return NotFound($"Recipe with ID {id} not found.");

            var recipeCategory = await _categoryService.GetCategoryByIdAsync(recipe.CategoryId);
            var recipeAuthor = await _userService.GetUserByIdAsync(recipe.AuthorId);

            var response = new RecipeResponse(
                recipe.RecipeId,
                recipe.Title,
                recipe.Description,
                recipe.Instructions,
                recipe.PreparationTime,
                recipe.Difficulty,
                recipe.ImageUrl,
                recipe.AuthorId,
                new UserResponse(recipeAuthor.Id, recipeAuthor.FirstName, recipeAuthor.LastName, recipeAuthor.Email),
                recipe.CategoryId,
                new CategoryResponse(recipeCategory.CategoryId, recipeCategory.Name),
                recipe.Ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name)).ToList()
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
                ImageUrl = request.ImageUrl,
            };


            var newIngredients = new List<Ingredient>();
            foreach (var ingredientId in request.IngredientIds)
            {
                var existingIngredient = await _ingredientService.GetIngredientByIdAsync(ingredientId);
                if (existingIngredient != null)
                {
                    newIngredients.Add(existingIngredient);
                }
            }
            newRecipe.Ingredients = newIngredients;

            var createdRecipe = await _recipeService.CreateRecipeAsync(newRecipe);
            if (createdRecipe == null)
                return BadRequest("Failed to create the recipe.");

            var authorResponse = new UserResponse(createdRecipe.Author.Id, createdRecipe.Author.FirstName, createdRecipe.Author.LastName, createdRecipe.Author.Email);
            var categoryResponse = new CategoryResponse(createdRecipe.Category.CategoryId, createdRecipe.Category.Name);
            var ingredientResponses = createdRecipe.Ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name)).ToList();

            return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.RecipeId }, new RecipeResponse(
                createdRecipe.RecipeId,
                createdRecipe.Title,
                createdRecipe.Description,
                createdRecipe.Instructions,
                createdRecipe.PreparationTime,
                createdRecipe.Difficulty,
                createdRecipe.ImageUrl,
                createdRecipe.AuthorId,
                authorResponse,
                createdRecipe.CategoryId,
                categoryResponse,
                ingredientResponses
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
            existingRecipe.Ingredients.Clear();

            foreach (var ingredientId in request.IngredientIds)
            {
                var existingIngredient = await _ingredientService.GetIngredientByIdAsync(ingredientId);
                if (existingIngredient != null)
                {
                    existingRecipe.Ingredients.Add(existingIngredient);
                }
            }

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
            foreach (var recipe in recipes)
            {
                var recipeCategory = await _categoryService.GetCategoryByIdAsync(recipe.CategoryId);
                var recipeAuthor = await _userService.GetUserByIdAsync(recipe.AuthorId);
                recipe.Category = recipeCategory;
                recipe.Author = recipeAuthor;
            }
            var response = recipes.Select(r => new RecipeResponse(
                r.RecipeId,
                r.Title,
                r.Description,
                r.Instructions,
                r.PreparationTime,
                r.Difficulty,
                r.ImageUrl,
                r.AuthorId,
                new UserResponse(r.Author.Id, r.Author.FirstName, r.Author.LastName, r.Author.Email),
                r.CategoryId,
                new CategoryResponse(r.Category.CategoryId, r.Category.Name),
                r.Ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name)).ToList()
            ));
            return Ok(response);
        }

        [HttpPost("{id}/ingredients")]
        public async Task<IActionResult> AddIngredientToRecipe(int id, [FromBody] int ingredientId)
        {
            try
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe == null)
                    return NotFound($"Recipe with ID {id} not found.");

                var ingredient = await _ingredientService.GetIngredientByIdAsync(ingredientId);
                if (ingredient == null)
                    return NotFound($"Ingredient with ID {ingredientId} not found.");

                recipe.Ingredients.Add(ingredient);
                await _recipeService.UpdateRecipeAsync(recipe);

                return NoContent();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }

        [HttpDelete("{id}/ingredients/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredientFromRecipe(int id, int ingredientId)
        {
            try
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe == null)
                    return NotFound($"Recipe with ID {id} not found.");

                var ingredient = recipe.Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
                if (ingredient == null)
                    return NotFound($"Ingredient with ID {ingredientId} not found in the recipe.");

                recipe.Ingredients.Remove(ingredient);
                await _recipeService.UpdateRecipeAsync(recipe);

                return NoContent();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }

        [HttpPost("{id}/favorites")]
        public async Task<IActionResult> AddRecipeToFavorites(int id, [FromBody] User user)
        {
            try
            {
                var favoriteRecipe = new FavoriteRecipe
                {
                    UserId = user.UserId,
                    RecipeId = id
                };

                await _favoriteRecipeService.AddFavoriteRecipeAsync(favoriteRecipe);
                return NoContent();
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
        }

        [HttpDelete("{id}/favorites/{userId}")]
        public async Task<IActionResult> RemoveRecipeFromFavorites(int id, string userId)
        {
            try
            {
                await _favoriteRecipeService.DeleteFavoriteRecipeAsync(userId, id);
                return NoContent();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
        [HttpGet("favorites/{userId}")]
        public async Task<IActionResult> GetFavorites(string userId)
        {
            var favorites = await _favoriteRecipeService.GetAllFavoriteRecipesAsync();
            var userFavorites = favorites.Where(f => f.UserId == userId);

            var recipeResponses = new List<RecipeResponse>();
            foreach (var favorite in userFavorites)
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(favorite.RecipeId);
                var recipeCategory = await _categoryService.GetCategoryByIdAsync(recipe.CategoryId);
                var recipeAuthor = await _userService.GetUserByIdAsync(recipe.AuthorId);
                recipe.Category = recipeCategory;
                recipe.Author = recipeAuthor;

                var ingredientResponses = recipe.Ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name)).ToList();
                var recipeResponse = new RecipeResponse(
                    recipe.RecipeId,
                    recipe.Title,
                    recipe.Description,
                    recipe.Instructions,
                    recipe.PreparationTime,
                    recipe.Difficulty,
                    recipe.ImageUrl,
                    recipe.AuthorId,
                    new UserResponse(recipeAuthor.Id, recipeAuthor.Email, recipeAuthor.FirstName, recipeAuthor.LastName),
                    recipe.CategoryId,
                    new CategoryResponse(recipeCategory.CategoryId, recipeCategory.Name),
                    ingredientResponses
                );
                recipeResponses.Add(recipeResponse);
            }

            return Ok(recipeResponses);
        }
    }
}

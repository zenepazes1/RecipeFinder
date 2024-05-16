using RecipeFinder.DataAccess.Repositories;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Application.Services
{
    public class RecipesService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(recipe.Title))
                throw new ArgumentException("Recipe title cannot be empty.");

            if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
                throw new ArgumentException("Recipes must include at least one ingredient.");

            return await _recipeRepository.AddAsync(recipe);
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            return recipe;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(recipe.Title))
                throw new ArgumentException("Recipe title cannot be empty.");

            await _recipeRepository.UpdateAsync(recipe);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _recipeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await _recipeRepository.GetAllAsync();
            //throw new ArgumentException("Search term cannot be empty.");

            return await _recipeRepository.SearchAsync(searchTerm);
        }
    }
}

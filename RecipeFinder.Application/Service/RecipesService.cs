using RecipeFinder.DataAccess.Repositories;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;

namespace RecipeFinder.Application.Service
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
    
            return await _recipeRepository.AddAsync(recipe);
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetByIdAsync(id);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.UpdateAsync(recipe);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _recipeRepository.DeleteAsync(id);
        }
    }
}

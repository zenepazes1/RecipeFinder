using RecipeFinder.DataAccess.Repositories;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Application.Services
{
    public class FavoriteRecipeService : IFavoriteRecipeService
    {
        private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;

        public FavoriteRecipeService(IFavoriteRecipeRepository favoriteRecipeRepository)
        {
            _favoriteRecipeRepository = favoriteRecipeRepository;
        }

        public async Task<FavoriteRecipe> AddFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe)
        {
            // Check if the recipe is already favorited by the user
            var existingFavorite = await _favoriteRecipeRepository.GetByIdAsync(favoriteRecipe.UserId, favoriteRecipe.RecipeId);
            if (existingFavorite != null)
            {
                throw new InvalidOperationException("This recipe is already in your favorites.");
            }

            return await _favoriteRecipeRepository.AddAsync(favoriteRecipe);
        }

        public async Task<FavoriteRecipe> GetFavoriteRecipeByIdAsync(string userId, int recipeId)
        {
            return await _favoriteRecipeRepository.GetByIdAsync(userId, recipeId);
        }

        public async Task<IEnumerable<FavoriteRecipe>> GetAllFavoriteRecipesAsync()
        {
            return await _favoriteRecipeRepository.GetAllAsync();
        }

        public async Task DeleteFavoriteRecipeAsync(string userId, int recipeId)
        {
            await _favoriteRecipeRepository.DeleteAsync(userId, recipeId);
        }
    }
}

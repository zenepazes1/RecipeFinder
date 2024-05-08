using RecipeFinder.DataAccess.Repositories;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;

namespace RecipeFinder.Application.Service
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
            return await _favoriteRecipeRepository.AddAsync(favoriteRecipe);
        }

        public async Task<FavoriteRecipe> GetFavoriteRecipeByIdAsync(int id)
        {
            return await _favoriteRecipeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<FavoriteRecipe>> GetAllFavoriteRecipesAsync()
        {
            return await _favoriteRecipeRepository.GetAllAsync();
        }

        public async Task UpdateFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe)
        {
            await _favoriteRecipeRepository.UpdateAsync(favoriteRecipe);
        }

        public async Task DeleteFavoriteRecipeAsync(int userId, int recipeId)
        {
            await _favoriteRecipeRepository.DeleteAsync(userId, recipeId);
        }
    }
}

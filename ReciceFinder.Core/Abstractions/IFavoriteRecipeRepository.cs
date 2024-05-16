using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IFavoriteRecipeRepository
    {
        Task<FavoriteRecipe> AddAsync(FavoriteRecipe favoriteRecipe);
        Task<FavoriteRecipe> GetByIdAsync(string userId, int recipeId);
        Task<IEnumerable<FavoriteRecipe>> GetAllAsync();
        Task DeleteAsync(string userId, int recipeId);
    }

}

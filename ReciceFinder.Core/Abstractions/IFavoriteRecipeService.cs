using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IFavoriteRecipeService
    {
        Task<FavoriteRecipe> AddFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe);
        Task<FavoriteRecipe> GetFavoriteRecipeByIdAsync(int id);
        Task<IEnumerable<FavoriteRecipe>> GetAllFavoriteRecipesAsync();
        Task UpdateFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe);
        Task DeleteFavoriteRecipeAsync(int userId, int recipeId);
    }
}

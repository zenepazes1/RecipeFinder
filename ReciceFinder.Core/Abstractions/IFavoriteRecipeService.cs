using RecipeFinder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Core.Abstractions
{
    public interface IFavoriteRecipeService
    {
        Task<FavoriteRecipe> AddFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe);
        Task<FavoriteRecipe> GetFavoriteRecipeByIdAsync(string userId, int recipeId);
        Task<IEnumerable<FavoriteRecipe>> GetAllFavoriteRecipesAsync();
        Task DeleteFavoriteRecipeAsync(string userId, int recipeId); 
    }
}

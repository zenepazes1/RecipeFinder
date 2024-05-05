using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IFavoriteRecipeRepository
    {
        Task<FavoriteRecipe> AddAsync(FavoriteRecipe favoriteRecipe);
        Task<FavoriteRecipe> GetByIdAsync(int id);
        Task<IEnumerable<FavoriteRecipe>> GetAllAsync();
        Task UpdateAsync(FavoriteRecipe favoriteRecipe);
        Task DeleteAsync(int id);
    }
}

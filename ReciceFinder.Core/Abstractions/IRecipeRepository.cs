using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IRecipeRepository
    {
        Task<Recipe> AddAsync(Recipe recipe);
        Task<Recipe> GetByIdAsync(int id);
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<IEnumerable<Recipe>> SearchAsync(string criteria);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int id);
    }
}

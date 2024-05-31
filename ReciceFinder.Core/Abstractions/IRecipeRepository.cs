using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IRecipeRepository
    {
        Task<Recipe> AddAsync(Recipe recipe);
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe> GetByIdAsync(int id);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int id);
        Task<IEnumerable<Recipe>> SearchAsync(string searchTerm);
    }
}

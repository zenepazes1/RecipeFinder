using RecipeFinder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Core.Abstractions
{
    public interface IRecipeService
    {
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
        Task<IEnumerable<Recipe>> SearchRecipesAsync(string searchTerm);
    }
}

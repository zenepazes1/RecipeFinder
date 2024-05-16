using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IIngredientRepository
    {
        Task<Ingredient> AddAsync(Ingredient ingredient);
        Task<Ingredient> GetByIdAsync(int id);
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(int id);
        Task<Ingredient> GetByNameAsync(string name);
    }
}

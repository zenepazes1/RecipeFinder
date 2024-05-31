using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IIngredientService
    {
        Task<Ingredient> CreateIngredientAsync(Ingredient ingredient);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int id);
        Task<Ingredient> GetByNameIngredientAsync(string name);
    }
}

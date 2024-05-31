using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions;

public interface IRecipeIngredientRepository
{
    Task AddAsync(RecipeIngredient recipeIngredient);
    Task<IEnumerable<RecipeIngredient>> GetAllAsync();
    Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(int recipeId);
    Task<IEnumerable<RecipeIngredient>> GetByIngredientIdAsync(int ingredientId);
    Task DeleteAsync(int recipeId, int ingredientId);
}

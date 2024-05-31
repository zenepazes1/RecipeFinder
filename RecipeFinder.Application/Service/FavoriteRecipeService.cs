using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;

public class FavoriteRecipeService : IFavoriteRecipeService
{
    private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;

    public FavoriteRecipeService(IFavoriteRecipeRepository favoriteRecipeRepository)
    {
        _favoriteRecipeRepository = favoriteRecipeRepository;
    }

    public async Task<FavoriteRecipe> AddFavoriteRecipeAsync(FavoriteRecipe favoriteRecipe)
    {
        var existingFavorite = await _favoriteRecipeRepository.GetByIdAsync(favoriteRecipe.UserId, favoriteRecipe.RecipeId);
        if (existingFavorite != null)
        {
            throw new InvalidOperationException("This recipe is already in your favorites.");
        }

        return await _favoriteRecipeRepository.AddAsync(favoriteRecipe);
    }

    public async Task<FavoriteRecipe> GetFavoriteRecipeByIdAsync(string userId, int recipeId)
    {
        return await _favoriteRecipeRepository.GetByIdAsync(userId, recipeId);
    }

    public async Task<IEnumerable<FavoriteRecipe>> GetAllFavoriteRecipesAsync()
    {
        return await _favoriteRecipeRepository.GetAllAsync();
    }

    public async Task DeleteFavoriteRecipeAsync(string userId, int recipeId)
    {
        await _favoriteRecipeRepository.DeleteAsync(userId, recipeId);
    }
}

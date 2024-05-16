using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Repositories
{
    public class FavoriteRecipeRepository : IFavoriteRecipeRepository
    {
        private readonly RecipeFinderDbContext _context;

        public FavoriteRecipeRepository(RecipeFinderDbContext context)
        {
            _context = context;
        }

        public async Task<FavoriteRecipe> AddAsync(FavoriteRecipe favoriteRecipe)
        {
            var favoriteRecipeEntity = new FavoriteRecipeEntity
            {
                UserId = favoriteRecipe.UserId,
                RecipeId = favoriteRecipe.RecipeId
            };

            await _context.FavoriteRecipes.AddAsync(favoriteRecipeEntity);
            await _context.SaveChangesAsync();

            return favoriteRecipe;  // Returns the model as the keys are already set
        }

        public async Task<FavoriteRecipe> GetByIdAsync(string userId, int recipeId)
        {
            var favoriteRecipeEntity = await _context.FavoriteRecipes
                .AsNoTracking()
                .FirstOrDefaultAsync(fr => fr.UserId == userId && fr.RecipeId == recipeId);

            if (favoriteRecipeEntity == null) return null;

            return new FavoriteRecipe
            {
                UserId = favoriteRecipeEntity.UserId,
                RecipeId = favoriteRecipeEntity.RecipeId
            };
        }

        public async Task<IEnumerable<FavoriteRecipe>> GetAllAsync()
        {
            return await _context.FavoriteRecipes
                .AsNoTracking()
                .Select(fr => new FavoriteRecipe
                {
                    UserId = fr.UserId,
                    RecipeId = fr.RecipeId
                }).ToListAsync();
        }

        public async Task UpdateAsync(FavoriteRecipe favoriteRecipe)
        {
            // Typically, there's not much to update in a join entity like FavoriteRecipe, 
            // unless you're tracking additional fields like 'addedOn', etc.
            // This method can be implemented if there's a need to update additional fields in the future.
        }

        public async Task DeleteAsync(string userId, int recipeId)
        {
            var favoriteRecipeEntity = await _context.FavoriteRecipes
                .FirstOrDefaultAsync(fr => fr.UserId == userId && fr.RecipeId == recipeId);

            if (favoriteRecipeEntity != null)
            {
                _context.FavoriteRecipes.Remove(favoriteRecipeEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

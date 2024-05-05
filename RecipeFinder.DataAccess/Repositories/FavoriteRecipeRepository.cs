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

            return favoriteRecipe;  // Возвращает модель без изменения, так как ключи уже установлены
        }

        public async Task<FavoriteRecipe> GetByIdAsync(int id)
        {

            return null;
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

        }

        public async Task DeleteAsync(int id)
        {
        }

        public async Task DeleteAsync(int userId, int recipeId)
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
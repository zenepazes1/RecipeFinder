using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly RecipeFinderDbContext _context;

        public IngredientRepository(RecipeFinderDbContext context)
        {
            _context = context;
        }

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            var ingredientEntity = new IngredientEntity
            {
                Name = ingredient.Name
            };

            await _context.Ingredients.AddAsync(ingredientEntity);
            await _context.SaveChangesAsync();

            ingredient.IngredientId = ingredientEntity.IngredientId; // Update ID after save
            return ingredient;
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            var ingredientEntity = await _context.Ingredients
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IngredientId == id);

            if (ingredientEntity == null) return null;

            return new Ingredient
            {
                IngredientId = ingredientEntity.IngredientId,
                Name = ingredientEntity.Name
            };
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients
                .AsNoTracking()
                .Select(i => new Ingredient
                {
                    IngredientId = i.IngredientId,
                    Name = i.Name
                }).ToListAsync();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            var ingredientEntity = await _context.Ingredients.FindAsync(ingredient.IngredientId);

            if (ingredientEntity != null)
            {
                ingredientEntity.Name = ingredient.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var ingredientEntity = await _context.Ingredients.FindAsync(id);

            if (ingredientEntity != null)
            {
                _context.Ingredients.Remove(ingredientEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

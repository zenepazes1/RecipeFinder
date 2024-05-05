using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeFinderDbContext _context;

        public RecipeRepository(RecipeFinderDbContext context)
        {
            _context = context;
        }

        public async Task<Recipe> AddAsync(Recipe recipe)
        {
            var recipeEntity = new RecipeEntity
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                PreparationTime = recipe.PreparationTime,
                Difficulty = recipe.Difficulty,
                AuthorId = recipe.AuthorId,
                CategoryId = recipe.CategoryId
            };

            await _context.Recipes.AddAsync(recipeEntity);
            await _context.SaveChangesAsync();

            recipe.RecipeId = recipeEntity.RecipeId;  // Update ID after save
            return recipe;
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipeEntity = await _context.Recipes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            if (recipeEntity == null) return null;

            var recipe = new Recipe
            {
                RecipeId = recipeEntity.RecipeId,
                Title = recipeEntity.Title,
                Description = recipeEntity.Description,
                Instructions = recipeEntity.Instructions,
                PreparationTime = recipeEntity.PreparationTime,
                Difficulty = recipeEntity.Difficulty,
                AuthorId = recipeEntity.AuthorId,
                CategoryId = recipeEntity.CategoryId
            };

            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var recipes = await _context.Recipes
                .AsNoTracking()
                .Select(r => new Recipe
                {
                    RecipeId = r.RecipeId,
                    Title = r.Title,
                    Description = r.Description,
                    Instructions = r.Instructions,
                    PreparationTime = r.PreparationTime,
                    Difficulty = r.Difficulty,
                    AuthorId = r.AuthorId,
                    CategoryId = r.CategoryId
                }).ToListAsync();

            return recipes;
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            var recipeEntity = await _context.Recipes
                .FirstOrDefaultAsync(r => r.RecipeId == recipe.RecipeId);

            if (recipeEntity != null)
            {
                recipeEntity.Title = recipe.Title;
                recipeEntity.Description = recipe.Description;
                recipeEntity.Instructions = recipe.Instructions;
                recipeEntity.PreparationTime = recipe.PreparationTime;
                recipeEntity.Difficulty = recipe.Difficulty;
                recipeEntity.AuthorId = recipe.AuthorId;
                recipeEntity.CategoryId = recipe.CategoryId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var recipeEntity = await _context.Recipes
                .FindAsync(id);

            if (recipeEntity != null)
            {
                _context.Recipes.Remove(recipeEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

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
                CategoryId = recipe.CategoryId,
                ImageUrl = recipe.ImageUrl
            };

            _context.Recipes.Add(recipeEntity);
            await _context.SaveChangesAsync();

            recipe.RecipeId = recipeEntity.RecipeId;
            return recipe;
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipeEntity = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            if (recipeEntity == null) return null;

            return new Recipe
            {
                RecipeId = recipeEntity.RecipeId,
                Title = recipeEntity.Title,
                Description = recipeEntity.Description,
                Instructions = recipeEntity.Instructions,
                PreparationTime = recipeEntity.PreparationTime,
                Difficulty = recipeEntity.Difficulty,
                AuthorId = recipeEntity.AuthorId,
                CategoryId = recipeEntity.CategoryId,
                ImageUrl = recipeEntity.ImageUrl
            };
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
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
                    CategoryId = r.CategoryId,
                    ImageUrl = r.ImageUrl
                }).ToListAsync();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            var recipeEntity = await _context.Recipes.FindAsync(recipe.RecipeId);

            if (recipeEntity != null)
            {
                _context.Entry(recipeEntity).CurrentValues.SetValues(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var recipeEntity = await _context.Recipes.FindAsync(id);

            if (recipeEntity != null)
            {
                _context.Recipes.Remove(recipeEntity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Recipe>> SearchAsync(string searchTerm)
        {
            return await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .Where(r => EF.Functions.Like(r.Title, $"%{searchTerm}%") ||
                            EF.Functions.Like(r.Description, $"%{searchTerm}%"))
                .Select(r => new Recipe
                {
                    RecipeId = r.RecipeId,
                    Title = r.Title,
                    Description = r.Description,
                    Instructions = r.Instructions,
                    PreparationTime = r.PreparationTime,
                    Difficulty = r.Difficulty,
                    AuthorId = r.AuthorId,
                    CategoryId = r.CategoryId,
                    ImageUrl = r.ImageUrl
                })
                .AsNoTracking()
                .ToListAsync();
        }

    }
}

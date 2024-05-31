using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                ImageUrl = recipe.ImageUrl,
                AuthorId = recipe.AuthorId,
                CategoryId = recipe.CategoryId
            };

            await _context.Recipes.AddAsync(recipeEntity);
            await _context.SaveChangesAsync();

            recipe.RecipeId = recipeEntity.RecipeId;
            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var recipeEntities = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.FavoriteRecipes)
                .ToListAsync();

            return recipeEntities.Select(MapToRecipe);
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipeEntity = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.FavoriteRecipes)
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            return recipeEntity == null ? null : MapToRecipe(recipeEntity);
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            var recipeEntity = await _context.Recipes.FindAsync(recipe.RecipeId);
            if (recipeEntity != null)
            {
                recipeEntity.Title = recipe.Title;
                recipeEntity.Description = recipe.Description;
                recipeEntity.Instructions = recipe.Instructions;
                recipeEntity.PreparationTime = recipe.PreparationTime;
                recipeEntity.Difficulty = recipe.Difficulty;
                recipeEntity.ImageUrl = recipe.ImageUrl;
                recipeEntity.AuthorId = recipe.AuthorId;
                recipeEntity.CategoryId = recipe.CategoryId;

                _context.Recipes.Update(recipeEntity);
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
            var recipeEntities = await _context.Recipes
                .Where(r => r.Title.Contains(searchTerm) || r.Description.Contains(searchTerm))
                .Include(r => r.Ingredients)
                .Include(r => r.FavoriteRecipes)
                .ToListAsync();

            return recipeEntities.Select(MapToRecipe);
        }

        private Recipe MapToRecipe(RecipeEntity entity)
        {
            return new Recipe
            {
                RecipeId = entity.RecipeId,
                Title = entity.Title,
                Description = entity.Description,
                Instructions = entity.Instructions,
                PreparationTime = entity.PreparationTime,
                Difficulty = entity.Difficulty,
                ImageUrl = entity.ImageUrl,
                AuthorId = entity.AuthorId,
                CategoryId = entity.CategoryId,
                Ingredients = entity.Ingredients.Select(i => new Ingredient
                {
                    IngredientId = i.IngredientId,
                    Name = i.Name
                }).ToList(),
                FavoriteRecipes = entity.FavoriteRecipes.Select(fr => new FavoriteRecipe
                {
                    UserId = fr.UserId,
                    RecipeId = fr.RecipeId
                }).ToList()
            };
        }
    }
}

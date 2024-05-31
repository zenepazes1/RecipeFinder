using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Repositories;

public class RecipeIngredientRepository : IRecipeIngredientRepository
{
    private readonly RecipeFinderDbContext _context;

    public RecipeIngredientRepository(RecipeFinderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RecipeIngredient recipeIngredient)
    {
        var recipeIngredientEntity = new RecipeIngredientEntity
        {
            RecipeId = recipeIngredient.RecipeId,
            IngredientId = recipeIngredient.IngredientId
        };

        await _context.RecipeIngredients.AddAsync(recipeIngredientEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RecipeIngredient>> GetAllAsync()
    {
        var recipeIngredientEntities = await _context.RecipeIngredients
            .Include(ri => ri.Recipe)
            .Include(ri => ri.Ingredient)
            .ToListAsync();

        return recipeIngredientEntities.Select(MapToRecipeIngredient);
    }

    public async Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(int recipeId)
    {
        var recipeIngredientEntities = await _context.RecipeIngredients
            .Include(ri => ri.Recipe)
            .Include(ri => ri.Ingredient)
            .Where(ri => ri.RecipeId == recipeId)
            .ToListAsync();

        return recipeIngredientEntities.Select(MapToRecipeIngredient);
    }

    public async Task<IEnumerable<RecipeIngredient>> GetByIngredientIdAsync(int ingredientId)
    {
        var recipeIngredientEntities = await _context.RecipeIngredients
            .Include(ri => ri.Recipe)
            .Include(ri => ri.Ingredient)
            .Where(ri => ri.IngredientId == ingredientId)
            .ToListAsync();

        return recipeIngredientEntities.Select(MapToRecipeIngredient);
    }

    public async Task DeleteAsync(int recipeId, int ingredientId)
    {
        var recipeIngredientEntity = await _context.RecipeIngredients
            .FirstOrDefaultAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == ingredientId);

        if (recipeIngredientEntity != null)
        {
            _context.RecipeIngredients.Remove(recipeIngredientEntity);
            await _context.SaveChangesAsync();
        }
    }

    private static RecipeIngredient MapToRecipeIngredient(RecipeIngredientEntity recipeIngredientEntity)
    {
        return new RecipeIngredient
        {
            RecipeId = recipeIngredientEntity.RecipeId,
            IngredientId = recipeIngredientEntity.IngredientId
        };
    }
}

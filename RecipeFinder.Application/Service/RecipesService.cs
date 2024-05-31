using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;

namespace RecipeFinder.Application.Services
{
    public class RecipesService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public RecipesService(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository, IRecipeIngredientRepository recipeIngredientRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _recipeIngredientRepository = recipeIngredientRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(recipe.Title))
                throw new ArgumentException("Recipe title cannot be empty.");

            if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
                throw new ArgumentException("Recipes must include at least one ingredient.");

            var newRecipe = await _recipeRepository.AddAsync(recipe);

            // Assign ingredients
            // Add ingredients to the recipe-ingredient table
            var newIngredients = new List<Ingredient>();
            foreach (var ingredient in recipe.Ingredients)
            {
                await _recipeIngredientRepository.AddAsync(new RecipeIngredient
                {
                    RecipeId = newRecipe.RecipeId,
                    IngredientId = ingredient.IngredientId
                });

                var existingIngredient = await _ingredientRepository.GetByIdAsync(ingredient.IngredientId);
                if (existingIngredient != null)
                {
                    newIngredients.Add(existingIngredient);
                }
            }
            newRecipe.Ingredients = newIngredients;
            newRecipe.Category = await _categoryRepository.GetByIdAsync(newRecipe.CategoryId);
            newRecipe.Author = await _userRepository.GetByIdAsync(newRecipe.AuthorId);

            await _recipeRepository.UpdateAsync(newRecipe);
            return newRecipe;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllAsync();
            foreach (var recipe in recipes)
            {
                recipe.Ingredients = await DefineIngredientsForRecipe(recipe);
                recipe.Category = await DefineCategoryForRecipe(recipe);
                recipe.Author = await DefineAuthorForRecipe(recipe);
            }
            return recipes;
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            recipe.Ingredients = await DefineIngredientsForRecipe(recipe);
            recipe.Category = await DefineCategoryForRecipe(recipe);
            recipe.Author = await DefineAuthorForRecipe(recipe);
            return recipe;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(recipe.Title))
                throw new ArgumentException("Recipe title cannot be empty.");

            // get all ingredients from recipe-ingredient table that belongs to current recipe
            var recipeIngredients = await _recipeIngredientRepository.GetByRecipeIdAsync(recipe.RecipeId);

            // remove the ingredients from the recipe-ingredient table
            foreach (var recipeIngredient in recipeIngredients)
            {
                await _recipeIngredientRepository.DeleteAsync(recipe.RecipeId, recipeIngredient.IngredientId);
            }

            // add the new ingredients to the recipe-ingredient table
            foreach (var ingredient in recipe.Ingredients)
            {
                await _recipeIngredientRepository.AddAsync(new RecipeIngredient
                {
                    RecipeId = recipe.RecipeId,
                    IngredientId = ingredient.IngredientId
                });
            }

            await _recipeRepository.UpdateAsync(recipe);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _recipeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string searchTerm)
        {
            var searchedRecipes = await _recipeRepository.SearchAsync(searchTerm);
            foreach (var recipe in searchedRecipes)
            {
                recipe.Ingredients = await DefineIngredientsForRecipe(recipe);
                recipe.Category = await DefineCategoryForRecipe(recipe);
                recipe.Author = await DefineAuthorForRecipe(recipe);
            }
            return searchedRecipes;
        }

        private async Task<List<Ingredient>> DefineIngredientsForRecipe(Recipe recipe)
        {
            var recipeIngredients = await _recipeIngredientRepository.GetByRecipeIdAsync(recipe.RecipeId);
            var ingredients = new List<Ingredient>();
            foreach (var recipeIngredient in recipeIngredients)
            {
                var ingredient = await _ingredientRepository.GetByIdAsync(recipeIngredient.IngredientId);
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        private async Task<Category> DefineCategoryForRecipe(Recipe recipe)
        {
            return await _categoryRepository.GetByIdAsync(recipe.CategoryId);
        }

        private async Task<ApplicationUser> DefineAuthorForRecipe(Recipe recipe)
        {
            return await _userRepository.GetByIdAsync(recipe.AuthorId);
        }
    }

}

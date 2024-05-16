using RecipeFinder.DataAccess.Repositories;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
        {
            if (ingredient == null)
                throw new ArgumentNullException(nameof(ingredient));

            if (string.IsNullOrWhiteSpace(ingredient.Name))
                throw new ArgumentException("Ingredient name cannot be empty.");

            var existingIngredient = await _ingredientRepository.GetByNameAsync(ingredient.Name);
            if (existingIngredient != null)
                throw new InvalidOperationException("An ingredient with the same name already exists.");

            return await _ingredientRepository.AddAsync(ingredient);
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await _ingredientRepository.GetAllAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            if (ingredient == null)
                return null;
            return ingredient;
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            if (ingredient == null)
                throw new ArgumentNullException(nameof(ingredient));

            if (string.IsNullOrWhiteSpace(ingredient.Name))
                throw new ArgumentException("Ingredient name cannot be empty.");

            await _ingredientRepository.UpdateAsync(ingredient);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _ingredientRepository.DeleteAsync(id);
        }

        public async Task<Ingredient> GetByNameIngredientAsync(string name)
        {
            
            return await _ingredientRepository.GetByNameAsync(name);
        }
    }
}

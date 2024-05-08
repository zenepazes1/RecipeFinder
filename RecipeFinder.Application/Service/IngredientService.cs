﻿using RecipeFinder.DataAccess.Repositories;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;

namespace RecipeFinder.Application.Service
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
            return await _ingredientRepository.AddAsync(ingredient);
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await _ingredientRepository.GetAllAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _ingredientRepository.GetByIdAsync(id);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.UpdateAsync(ingredient);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _ingredientRepository.DeleteAsync(id);
        }
    }
}

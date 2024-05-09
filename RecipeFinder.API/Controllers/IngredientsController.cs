using Microsoft.AspNetCore.Mvc;
using RecipeFinder.API.Contracts;
using RecipeFinder.Application.Service;
using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientService _ingredientService;

        public IngredientsController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientResponse>>> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            var response = ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name)).ToList();
            return Ok(response);
        }

        // GET: api/Ingredients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientResponse>> GetIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with ID {id} not found.");
            }
            return Ok(new IngredientResponse(ingredient.IngredientId, ingredient.Name));
        }

        // POST: api/Ingredients
        [HttpPost]
        public async Task<ActionResult<IngredientResponse>> CreateIngredient([FromBody] IngredientRequest request)
        {
            var ingredient = new Ingredient { Name = request.Name };
            var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredient);
            return CreatedAtAction(nameof(GetIngredient), new { id = createdIngredient.IngredientId }, new IngredientResponse(createdIngredient.IngredientId, createdIngredient.Name));
        }

        // PUT: api/Ingredients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientRequest request)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with ID {id} not found.");
            }
            ingredient.Name = request.Name;
            await _ingredientService.UpdateIngredientAsync(ingredient);
            return NoContent();
        }

        // DELETE: api/Ingredients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            await _ingredientService.DeleteIngredientAsync(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeFinder.Core.Models;
using RecipeFinder.Core.Abstractions;
using System.Threading.Tasks;
using RecipeFinder.API.Contracts;

namespace RecipeFinder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            var response = ingredients.Select(i => new IngredientResponse(i.IngredientId, i.Name));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
                return NotFound($"Ingredient with ID {id} not found.");
            var response = new IngredientResponse(ingredient.IngredientId, ingredient.Name);
            return Ok(response);

        }

        [HttpPost]
        //[Authorize(Roles = "Admin, User")] // Both admins and users can add ingredients
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredient = new Ingredient { Name = request.Name };
            var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredient);
            if (createdIngredient == null)
                return BadRequest("Failed to create the ingredient.");

            var response = new IngredientResponse(createdIngredient.IngredientId, createdIngredient.Name);
            return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.IngredientId }, response);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")] // Assuming only admins can update ingredients
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingIngredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (existingIngredient == null)
                return NotFound($"Ingredient with ID {id} not found.");

            existingIngredient.Name = request.Name;
            await _ingredientService.UpdateIngredientAsync(existingIngredient);
            return NoContent();

        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")] // Assuming only admins can delete ingredients
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                await _ingredientService.DeleteIngredientAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
        /*[HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        *//*[HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
                return Ok(ingredient);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }*//*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
                return NotFound($"Ingredient with ID {id} not found.");
            return Ok(ingredient);
        }

        *//*[HttpPost]
        [Authorize(Roles = "Admin, User")] // Both admins and users can add ingredients
        public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
        {
            try
            {
                var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredient);
                return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.IngredientId }, createdIngredient);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
        }*//*
        [HttpPost]
        [Authorize(Roles = "Admin, User")] // Both admins and users can add ingredients
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredient = new Ingredient { Name = request.Name };
            var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredient);
            if (createdIngredient == null)
                return BadRequest("Failed to create the ingredient.");
            return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.IngredientId }, createdIngredient);
        }

        *//*[HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Assuming only admins can update ingredients
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] Ingredient ingredient)
        {
            try
            {
                await _ingredientService.UpdateIngredientAsync(ingredient);
                return NoContent();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }*//*
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Assuming only admins can update ingredients
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingIngredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (existingIngredient == null)
                return NotFound($"Ingredient with ID {id} not found.");

            existingIngredient.Name = request.Name;
            await _ingredientService.UpdateIngredientAsync(existingIngredient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Assuming only admins can delete ingredients
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                await _ingredientService.DeleteIngredientAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }

        *//*[HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Assuming only admins can delete ingredients
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var success = await _ingredientService.DeleteIngredientAsync(id);
            if (!success)
                return NotFound($"Ingredient with ID {id} not found.");

            return NoContent();
        }*/
    }
}


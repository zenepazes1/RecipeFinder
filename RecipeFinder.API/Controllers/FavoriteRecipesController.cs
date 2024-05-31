/*using Microsoft.AspNetCore.Mvc;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteRecipesController : ControllerBase
    {
        private readonly IFavoriteRecipeService _favoriteRecipeService;

        public FavoriteRecipesController(IFavoriteRecipeService favoriteRecipeService)
        {
            _favoriteRecipeService = favoriteRecipeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRecipeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var favoriteRecipe = new FavoriteRecipe
            {
                UserId = request.UserId,
                RecipeId = request.RecipeId
            };

            try
            {
                var createdFavorite = await _favoriteRecipeService.AddFavoriteRecipeAsync(favoriteRecipe);
                return Ok(createdFavorite);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRecipeRequest request)
        {
            await _favoriteRecipeService.DeleteFavoriteRecipeAsync(request.UserId, request.RecipeId);
            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavorites(string userId)
        {
            var favorites = await _favoriteRecipeService.GetAllFavoriteRecipesAsync();
            var userFavorites = favorites.Where(f => f.UserId == userId);
            return Ok(userFavorites);
        }
    }

}
*/
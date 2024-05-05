using Microsoft.AspNetCore.Mvc;

namespace RecipeFinder.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class RecipeController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public RecipeController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetRecipes()
        {
            var response;

            return Ok(response);
        }
    }
}

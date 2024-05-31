using RecipeFinder.Core.Models;

namespace RecipeFinder.API.Contracts
{
    public record RecipeRequest(
        string Title,
        string Description,
        string Instructions,
        int PreparationTime,
        int Difficulty,
        string AuthorId,
        int CategoryId,
        string ImageUrl,

        List<int> IngredientIds
    );
}

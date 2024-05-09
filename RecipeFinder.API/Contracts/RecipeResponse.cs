namespace RecipeFinder.API.Contracts
{
    public record RecipeResponse(
        int RecipeId,
        string Title,
        string Description,
        string Instructions,
        int PreparationTime,
        int Difficulty,
        int AuthorId,
        int CategoryId,
        string ImageUrl);  // DTO for response
}

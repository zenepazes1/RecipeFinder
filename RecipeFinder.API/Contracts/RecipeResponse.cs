namespace RecipeFinder.API.Contracts
{
    public record RecipeResponse(
        int RecipeId,
        string Title,
        string Description,
        string Instructions,
        int PreparationTime,
        int Difficulty,
        string ImageUrl,

        string AuthorId,
        UserResponse Author,
        int CategoryId,
        CategoryResponse Category,
        
        ICollection<IngredientResponse> Ingredients
    );
}

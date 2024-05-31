namespace RecipeFinder.DataAccess.Entities;

public class RecipeIngredientEntity
{
    public int RecipeId { get; set; }
    public RecipeEntity? Recipe { get; set; }

    public int IngredientId { get; set; }
    public IngredientEntity? Ingredient { get; set; }
}

namespace RecipeFinder.Core.Models
{
    public class FavoriteRecipe
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public FavoriteRecipe(int userId, int recipeId)
        {
            UserId = userId;
            RecipeId = recipeId;
        }
    }
}

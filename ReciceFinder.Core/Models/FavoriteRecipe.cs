namespace RecipeFinder.Core.Models
{
    public class FavoriteRecipe
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }

}

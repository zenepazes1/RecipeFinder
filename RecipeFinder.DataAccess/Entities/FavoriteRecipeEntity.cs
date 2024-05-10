namespace RecipeFinder.DataAccess.Entities
{
    public class FavoriteRecipeEntity
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual ApplicationUserEntity User { get; set; }
        public virtual RecipeEntity Recipe { get; set; }
    }
}

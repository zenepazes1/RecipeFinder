namespace RecipeFinder.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();
    }


}

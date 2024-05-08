namespace RecipeFinder.DataAccess.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation
        public virtual ICollection<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
        public virtual ICollection<FavoriteRecipeEntity> FavoriteRecipes { get; set; } = new List<FavoriteRecipeEntity>();  // Добавлено навигационное свойство
    }

}

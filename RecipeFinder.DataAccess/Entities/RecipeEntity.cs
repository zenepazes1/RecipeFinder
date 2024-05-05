namespace RecipeFinder.DataAccess.Entities
{
    public class RecipeEntity
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; }
        public int CategoryId { get; set; }
        public int Difficulty { get; set; }
        public int AuthorId { get; set; }

        // Навигационные свойства
        public virtual CategoryEntity Category { get; set; }
        public virtual UserEntity Author { get; set; }
        public virtual ICollection<FavoriteRecipeEntity> FavoriteRecipes { get; set; } = new List<FavoriteRecipeEntity>();  // Добавлено навигационное свойство
    }
}

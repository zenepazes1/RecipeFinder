namespace RecipeFinder.DataAccess.Entities
{
    public class RecipeEntity
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; } // Может быть в минутах или другом формате времени
        public string Category { get; set; }
        public int Difficulty { get; set; } // Может быть от 1 до 5, например
        public int AuthorId { get; set; } // Внешний ключ на пользователя
        //public User Author { get; set; } // Навигационное свойство к пользователю-автору

    }
}

namespace RecipeFinder.Core.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; } // Может быть в минутах или другом формате времени
        public string Category { get; set; }
        public int Difficulty { get; set; } // Может быть от 1 до 5, например
        public int AuthorId { get; set; } // Внешний ключ на пользователя

        public Recipe(int recipeId, string title, string description, string instructions, int preparationTime, string category, int difficulty, int authorId)
        {
            RecipeId = recipeId;
            Title = title;
            Description = description;
            Instructions = instructions;
            PreparationTime = preparationTime;
            Category = category;
            Difficulty = difficulty;
            AuthorId = authorId;
        }
    }
}

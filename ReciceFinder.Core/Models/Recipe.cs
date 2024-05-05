namespace RecipeFinder.Core.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; }
        public int Difficulty { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }

}

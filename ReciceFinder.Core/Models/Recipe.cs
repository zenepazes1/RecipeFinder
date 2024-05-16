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
        public string AuthorId { get; set; } 
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();
    }
}

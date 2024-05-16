using System.Collections.Generic;

namespace RecipeFinder.DataAccess.Entities
{
    public class RecipeEntity
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; }
        public int Difficulty { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorId { get; set; } // Changed to string to match IdentityUser primary key type
        public int CategoryId { get; set; }

        public virtual ApplicationUserEntity Author { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual ICollection<IngredientEntity> Ingredients { get; set; } = new List<IngredientEntity>();
        public virtual ICollection<FavoriteRecipeEntity> FavoriteRecipes { get; set; } = new List<FavoriteRecipeEntity>();
    }
}

using System.Collections.Generic;

namespace RecipeFinder.DataAccess.Entities
{
    public class IngredientEntity
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
        public virtual ICollection<RecipeIngredientEntity> RecipeIngredients { get; set; } = new List<RecipeIngredientEntity>();
    }
}

namespace RecipeFinder.Core.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }

}

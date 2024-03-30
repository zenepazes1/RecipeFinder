namespace RecipeFinder.Core.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public Ingredient(int ingredientId, string name)
        {
            IngredientId = ingredientId;
            Name = name;
        }
    }
}

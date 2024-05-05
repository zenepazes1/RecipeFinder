namespace RecipeFinder.DataAccess.Entities
{
    public class CategoryEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
    }

}

using Microsoft.AspNetCore.Identity;

namespace RecipeFinder.DataAccess.Entities
{
    public class ApplicationUserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
        public virtual ICollection<FavoriteRecipeEntity> FavoriteRecipes { get; set; } = new List<FavoriteRecipeEntity>();
    }
}

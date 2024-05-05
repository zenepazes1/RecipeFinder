using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Configurations;

namespace RecipeFinder.DataAccess
{
    public class RecipeFinderDbContext : DbContext
    {
        public RecipeFinderDbContext(DbContextOptions<RecipeFinderDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecipeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteRecipeEntityConfiguration());
        }

        // DbSet properties
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
    }

}

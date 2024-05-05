using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Configurations;
using RecipeFinder.DataAccess.Entities;

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
        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<FavoriteRecipeEntity> FavoriteRecipes { get; set; }
    }

}

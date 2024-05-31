using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Configurations;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess
{
    public class RecipeFinderDbContext : IdentityDbContext<ApplicationUserEntity>
    {
        public RecipeFinderDbContext(DbContextOptions<RecipeFinderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteRecipeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeIngredientEntityConfiguration());
        }

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<FavoriteRecipeEntity> FavoriteRecipes { get; set; }
        public DbSet<RecipeIngredientEntity> RecipeIngredients { get; set; }
    }

}

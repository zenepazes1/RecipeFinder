using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Configurations
{
    public class FavoriteRecipeEntityConfiguration : IEntityTypeConfiguration<FavoriteRecipeEntity>
    {
        public void Configure(EntityTypeBuilder<FavoriteRecipeEntity> builder)
        {
            builder.HasKey(fr => new { fr.UserId, fr.RecipeId });
            builder.HasOne(fr => fr.User)
                .WithMany(u => u.FavoriteRecipes)
                .HasForeignKey(fr => fr.UserId);
            builder.HasOne(fr => fr.Recipe)
                .WithMany(r => r.FavoriteRecipes)
                .HasForeignKey(fr => fr.RecipeId);
        }
    }
}

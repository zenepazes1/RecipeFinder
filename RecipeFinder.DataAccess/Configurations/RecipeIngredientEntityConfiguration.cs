using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Configurations;

public class RecipeIngredientEntityConfiguration : IEntityTypeConfiguration<RecipeIngredientEntity>
{
    public void Configure(EntityTypeBuilder<RecipeIngredientEntity> builder)
    {
        builder.HasKey(ri => new { ri.RecipeId, ri.IngredientId });

        builder.HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

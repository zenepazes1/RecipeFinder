using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Configurations
{
    public class RecipeEntityConfiguration : IEntityTypeConfiguration<RecipeEntity>
    {
        public void Configure(EntityTypeBuilder<RecipeEntity> builder)
        {
            builder.HasKey(r => r.RecipeId);

            builder.Property(r => r.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(r => r.Description)
                   .IsRequired();

            builder.Property(r => r.Instructions)
                   .IsRequired();

            builder.Property(r => r.PreparationTime);

            builder.Property(r => r.Difficulty);

            builder.Property(r => r.AuthorId)
                   .IsRequired();

            builder.Property(r => r.CategoryId)
                   .IsRequired();

            builder.Property(r => r.ImageUrl)
                   .HasMaxLength(500);

            builder.HasOne(r => r.Category)
                   .WithMany(c => c.Recipes)
                   .HasForeignKey(r => r.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Author)
                   .WithMany(u => u.Recipes)
                   .HasForeignKey(r => r.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
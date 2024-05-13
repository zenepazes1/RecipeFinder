using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Configurations
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUserEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserEntity> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);

            // Настройка связей
            builder.HasMany(u => u.Recipes) 
                .WithOne(r => r.Author) 
                .HasForeignKey(r => r.AuthorId) 
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(u => u.FavoriteRecipes) 
                .WithOne(fr => fr.User) 
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
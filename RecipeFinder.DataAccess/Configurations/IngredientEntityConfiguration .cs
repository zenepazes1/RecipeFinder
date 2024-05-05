using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RecipeFinder.DataAccess.Entities;


namespace RecipeFinder.DataAccess.Configurations
{
    public class IngredientEntityConfiguration : IEntityTypeConfiguration<IngredientEntity>
    {
        public void Configure(EntityTypeBuilder<IngredientEntity> builder)
        {
            builder.HasKey(i => i.IngredientId);

            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}

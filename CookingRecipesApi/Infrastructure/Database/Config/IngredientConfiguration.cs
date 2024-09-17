using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure( EntityTypeBuilder<Ingredient> builder )
    {
        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id );

        builder.Property( x => x.Name )
            .IsRequired( true );

        builder.Property( x => x.Product )
            .IsRequired( true );

        builder.Property( a => a.RecipeId );

        builder.HasOne( x => x.Recipe )
           .WithMany( x => x.Ingredients )
            .HasForeignKey( x => x.RecipeId );
    }
}

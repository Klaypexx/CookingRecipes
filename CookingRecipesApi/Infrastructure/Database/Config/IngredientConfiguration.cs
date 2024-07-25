using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;
public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure( EntityTypeBuilder<Ingredient> builder )
    {
        builder.ToTable( "ingredient" );

        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id )
            .HasColumnName( "id_ingredient" );

        builder.Property( x => x.Name )
            .HasColumnName( "name" )
            .IsRequired( true );

        builder.Property( x => x.Product )
            .HasColumnName( "product" )
            .IsRequired( true );

        builder.Property( a => a.RecipeId )
            .HasColumnName( "id_recipe" );

        builder.HasOne( x => x.Recipe )
           .WithMany( x => x.Ingredients )
            .HasForeignKey( x => x.RecipeId );
    }
}

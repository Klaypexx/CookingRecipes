using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Config;

public class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
{
    public void Configure( EntityTypeBuilder<RecipeTag> builder )
    {
        builder.ToTable( "recipe_tag" );

        builder.HasKey( x => new { x.RecipeId, x.TagId } );

        builder.Property( x => x.RecipeId )
            .HasColumnName( "id_recipe" );

        builder.Property( x => x.TagId )
            .HasColumnName( "id_tag" );

        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.Tags )
            .HasForeignKey( x => x.RecipeId )
            .OnDelete( DeleteBehavior.Cascade );

        builder.HasOne( x => x.Tag )
            .WithMany( x => x.Recipes )
            .HasForeignKey( x => x.TagId );
    }
}
using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
{
    public void Configure( EntityTypeBuilder<RecipeTag> builder )
    {
        builder.HasKey( x => new { x.RecipeId, x.TagId } );

        builder.Property( x => x.RecipeId );

        builder.Property( x => x.TagId );

        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.Tags )
            .HasForeignKey( x => x.RecipeId );

        builder.HasOne( x => x.Tag )
            .WithMany( x => x.Recipes )
            .HasForeignKey( x => x.TagId );
    }
}
using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure( EntityTypeBuilder<Step> builder )
    {
        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id );

        builder.Property( x => x.Description )
           .IsRequired( true );

        builder.Property( x => x.RecipeId );

        builder.HasOne( x => x.Recipe )
          .WithMany( x => x.Steps )
          .HasForeignKey( x => x.RecipeId );

    }
}

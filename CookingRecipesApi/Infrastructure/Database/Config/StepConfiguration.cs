using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Config;

public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure( EntityTypeBuilder<Step> builder )
    {
        builder.ToTable( "step" );
        builder.HasKey( x => x.Id );

        builder.Property( x => x.StepNumber )
           .HasColumnName( "step_number" )
           .IsRequired( true );

        builder.Property( x => x.Description )
           .HasColumnName( "name" )
           .IsRequired( true );

        builder.HasOne( x => x.Recipe )
          .WithMany( x => x.Steps );

    }
}

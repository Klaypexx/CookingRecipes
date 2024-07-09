using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Config;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure( EntityTypeBuilder<Recipe> builder )
    {
        builder.ToTable( "recipe" );
        builder.HasKey( x => x.Id );

        builder.Property( x => x.Name )
            .HasColumnName( "name" )
            .IsRequired( true );

        builder.Property( x => x.ShortDescription )
            .HasColumnName( "description" )
            .IsRequired( false );

        builder.Property( x => x.CookingTime )
            .HasColumnName( "time" )
            .IsRequired( false );

        builder.Property( x => x.Portion )
           .HasColumnName( "portion" )
           .IsRequired( false );

        builder.Property( x => x.Portion )
           .HasColumnName( "portion" )
           .IsRequired( false );

        builder.Property( x => x.Like )
           .HasColumnName( "like" )
           .IsRequired( false );

        builder.HasMany( x => x.Tags )
           .WithMany( x => x.Recipes );

        builder.HasMany( x => x.Ingredients )
           .WithOne( x => x.Recipe );

        builder.HasMany( x => x.Steps )
          .WithOne( x => x.Recipe );

        builder.HasOne( x => x.Author )
            .WithMany( x => x.Recipes );

        builder.HasMany( x => x.FavouritedBy )
            .WithMany( x => x.Favourites );
    }
}

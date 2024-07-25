using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Config;
public class FavouriteRecipeConfiguration : IEntityTypeConfiguration<FavouriteRecipe>
{
    public void Configure( EntityTypeBuilder<FavouriteRecipe> builder )
    {
        builder.ToTable( "favourite_recipe" );

        builder.HasKey( x => new { x.UserId, x.RecipeId } );

        builder.Property( x => x.UserId )
            .HasColumnName( "id_user" );

        builder.Property( x => x.RecipeId )
            .HasColumnName( "id_recipe" );

        builder.HasOne( x => x.User )
            .WithMany( x => x.FavouriteRecipes )
            .HasForeignKey( x => x.UserId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.FavouritedBy )
            .HasForeignKey( x => x.RecipeId )
            .OnDelete( DeleteBehavior.Restrict );
    }
}

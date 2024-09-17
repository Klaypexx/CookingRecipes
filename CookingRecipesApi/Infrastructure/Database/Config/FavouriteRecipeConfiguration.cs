using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class FavouriteRecipeConfiguration : IEntityTypeConfiguration<FavouriteRecipe>
{
    public void Configure( EntityTypeBuilder<FavouriteRecipe> builder )
    {
        builder.HasKey( x => new { x.UserId, x.RecipeId } );

        builder.Property( x => x.UserId );

        builder.Property( x => x.RecipeId );

        builder.HasOne( x => x.User )
            .WithMany( x => x.FavouriteRecipes )
            .HasForeignKey( x => x.UserId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.FavouriteRecipes )
            .HasForeignKey( x => x.RecipeId )
            .OnDelete( DeleteBehavior.Restrict );
    }
}

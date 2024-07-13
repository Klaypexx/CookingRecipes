using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Config;
public class FavouriteRecipeConfiguration : IEntityTypeConfiguration<FavouriteRecipe>
{
    public void Configure( EntityTypeBuilder<FavouriteRecipe> builder )
    {
        builder.HasKey( x => new { x.UserId, x.RecipeId } );
        builder.HasOne( x => x.User )
            .WithMany( x => x.FavouriteRecipes )
            .HasForeignKey( x => x.UserId );
        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.FavouritedBy )
            .HasForeignKey( x => x.RecipeId );
    }
}

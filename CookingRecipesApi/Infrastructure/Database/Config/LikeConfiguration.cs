using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;
public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure( EntityTypeBuilder<Like> builder )
    {
        builder.ToTable( "like" );

        builder.HasKey( x => new { x.UserId, x.RecipeId } );

        builder.Property( x => x.UserId )
            .HasColumnName( "id_user" );

        builder.Property( x => x.RecipeId )
            .HasColumnName( "id_recipe" );

        builder.HasOne( x => x.User )
            .WithMany( x => x.Likes )
            .HasForeignKey( x => x.UserId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.Likes )
            .HasForeignKey( x => x.RecipeId )
            .OnDelete( DeleteBehavior.Restrict );
    }
}

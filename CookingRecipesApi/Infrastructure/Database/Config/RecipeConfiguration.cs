using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    private const int _nameMaxLenght = 50;
    private const int _descriptionMaxLenght = 150;

    public void Configure( EntityTypeBuilder<Recipe> builder )
    {
        builder.ToTable( "recipe" );

        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id )
            .HasColumnName( "id_recipe" );

        builder.Property( x => x.Name )
            .HasColumnName( "name" )
            .HasMaxLength( _nameMaxLenght )
            .IsRequired( true );

        builder.Property( x => x.Description )
            .HasColumnName( "description" )
            .HasMaxLength( _descriptionMaxLenght )
            .IsRequired( true );

        builder.Property( x => x.Avatar )
            .HasColumnName( "avatar" );

        builder.Property( x => x.CookingTime )
            .HasColumnName( "time" )
            .IsRequired( true );

        builder.Property( x => x.Portion )
           .HasColumnName( "portion" )
           .IsRequired( true );

        builder.Property( x => x.AuthorId )
           .HasColumnName( "id_author" )
           .IsRequired( true );

        builder.HasMany( x => x.Tags )
            .WithOne( x => x.Recipe )
            .HasForeignKey( x => x.RecipeId );

        builder.HasMany( x => x.Ingredients )
           .WithOne( x => x.Recipe )
           .HasForeignKey( x => x.RecipeId );

        builder.HasMany( x => x.Steps )
          .WithOne( x => x.Recipe )
          .HasForeignKey( x => x.RecipeId );

        builder.HasOne( x => x.Author )
            .WithMany( x => x.Recipes )
            .HasForeignKey( x => x.AuthorId );

        builder.HasMany( x => x.FavouriteRecipes )
            .WithOne( x => x.Recipe )
            .HasForeignKey( x => x.RecipeId );

        builder.HasMany( x => x.Likes )
            .WithOne( x => x.Recipe )
            .HasForeignKey( x => x.RecipeId );
    }
}

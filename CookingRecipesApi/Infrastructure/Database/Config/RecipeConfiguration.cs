﻿using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Config;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure( EntityTypeBuilder<Recipe> builder )
    {
        builder.ToTable( "recipe" );

        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id )
            .HasColumnName( "id_recipe" );

        builder.Property( x => x.Name )
            .HasColumnName( "name" )
            .IsRequired( true );

        builder.Property( x => x.Description )
            .HasColumnName( "description" )
            .IsRequired( true );

        builder.Property( x => x.Avatar )
            .HasColumnName( "avatar" )
            .IsRequired( false );

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

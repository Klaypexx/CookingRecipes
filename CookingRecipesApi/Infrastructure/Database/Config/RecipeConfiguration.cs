using Domain.Auth.Entities;
using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Config;
public class RecipeConfiguration: IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("recipe");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("recipe_id")
            .ValueGeneratedNever();

        builder.Property(a => a.Name)
            .HasColumnName("name")
            .IsRequired(true);

        builder.Property(a => a.ShortDescription)
            .HasColumnName("description")
            .IsRequired(false);

        builder.Property(a => a.CookingTime)
            .HasColumnName("time")
            .IsRequired(false);

        builder.Property(a => a.Portion)
           .HasColumnName("portion")
           .IsRequired(false);

        builder.Property(a => a.Portion)
           .HasColumnName("portion")
           .IsRequired(false);

        builder.Property(a => a.Like)
           .HasColumnName("like")
           .IsRequired(false);

        builder.HasMany(u => u.Tags)
           .WithMany(a => a.Recipes);

        builder.HasMany(u => u.Ingredients)
           .WithOne(a => a.Recipe);

        builder.HasMany(u => u.StepDescriptions)
          .WithOne(a => a.Recipe);

        builder.HasOne(u => u.User)
            .WithMany(a => a.Recipes);

        builder.HasMany(u => u.FavouritedBy)
            .WithMany(a => a.Favourites);
    }
}

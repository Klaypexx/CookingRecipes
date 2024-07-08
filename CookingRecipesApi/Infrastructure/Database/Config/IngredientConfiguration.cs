using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Config;
public class IngredientConfiguration: IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("ingredient");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasColumnName("name")
            .IsRequired(true);

        builder.Property(a => a.Product)
            .HasColumnName("product")
            .IsRequired(true);

        builder.HasOne(u => u.Recipe)
           .WithMany(a => a.Ingredients);
    }
}

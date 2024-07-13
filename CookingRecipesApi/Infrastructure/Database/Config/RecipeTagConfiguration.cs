using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Config;

public class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
{
    public void Configure( EntityTypeBuilder<RecipeTag> builder )
    {
        builder.HasKey( x => new { x.RecipeId, x.TagId } );
        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.Tags )
            .HasForeignKey( x => x.RecipeId );
        builder.HasOne( x => x.Tag )
            .WithMany( x => x.Recipes )
            .HasForeignKey( x => x.TagId );
    }
}
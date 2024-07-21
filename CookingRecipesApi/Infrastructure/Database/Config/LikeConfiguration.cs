using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;
public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure( EntityTypeBuilder<Like> builder )
    {
        builder.HasKey( x => new { x.UserId, x.RecipeId } );
        builder.HasOne( x => x.User )
            .WithMany( x => x.Likes )
            .HasForeignKey( x => x.UserId );
        builder.HasOne( x => x.Recipe )
            .WithMany( x => x.LikesCount )
            .HasForeignKey( x => x.RecipeId );
    }
}

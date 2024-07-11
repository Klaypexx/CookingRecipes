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
        builder.ToTable( "like" );
        builder.HasKey( x => x.Id );

        builder.Property( x => x.Count )
            .HasColumnName( "count" )
            .IsRequired( true )
            .HasDefaultValue( 0 );

        builder.HasOne( x => x.Recipe )
            .WithOne( x => x.LikesCount );
    }
}

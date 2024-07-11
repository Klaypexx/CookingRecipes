using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Config;
public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
{
    public void Configure( EntityTypeBuilder<Favourite> builder )
    {
        builder.ToTable( "favourite" );
        builder.HasKey( x => x.Id );

        builder.Property( x => x.Count )
            .HasColumnName( "count" )
            .IsRequired( true )
            .HasDefaultValue( 0 );

        builder.HasOne( x => x.Recipe )
            .WithOne( x => x.FavouritesCount );
    }
}

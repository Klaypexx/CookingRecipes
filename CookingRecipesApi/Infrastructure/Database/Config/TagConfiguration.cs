﻿using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure( EntityTypeBuilder<Tag> builder )
    {
        builder.ToTable( "tag" );
        builder.HasKey( x => x.Id );

        builder.Property( x => x.Name )
           .HasColumnName( "name" )
           .IsRequired( true );

        builder.HasMany( x => x.Recipes )
          .WithMany( x => x.Tags );
    }
}

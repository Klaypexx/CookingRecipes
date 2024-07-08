﻿using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Config;
public class TagConfiguration: IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tag");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("tag_id")
            .ValueGeneratedNever();

        builder.Property(a => a.Name)
           .HasColumnName("name")
           .IsRequired(true);

        builder.HasMany(u => u.Recipes)
          .WithMany(a => a.Tags);

    }
}

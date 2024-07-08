using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Config;
public class StepDescriptionConfiguration: IEntityTypeConfiguration<StepDescription>
{
    public void Configure(EntityTypeBuilder<StepDescription> builder)
    {
        builder.ToTable("step");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Description)
           .HasColumnName("name")
           .IsRequired(true);

        builder.HasOne(u => u.Recipe)
          .WithMany(a => a.StepDescriptions);

    }
}

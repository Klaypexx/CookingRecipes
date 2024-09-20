using Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure( EntityTypeBuilder<Tag> builder )
    {
        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id );

        builder.Property( x => x.Name )
           .IsRequired( true );

        builder.HasMany( x => x.Recipes )
            .WithOne( x => x.Tag )
            .HasForeignKey( x => x.TagId );
    }
}

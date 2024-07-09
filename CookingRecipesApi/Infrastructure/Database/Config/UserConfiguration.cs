using Domain.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure( EntityTypeBuilder<User> builder )
    {
        builder.ToTable( "user" );
        builder.HasKey( x => x.Id );

        builder.Property( x => x.Name )
            .HasColumnName( "name" )
            .IsRequired( true );

        builder.Property( x => x.UserName )
            .HasColumnName( "username" )
            .IsRequired( true );

        builder.Property( x => x.Password )
            .HasColumnName( "password" )
            .IsRequired( true );

        builder.Property( x => x.AvatarPath )
           .HasColumnName( "avatar" )
           .IsRequired( false );

        builder.HasIndex( x => x.UserName )
            .IsUnique();

        builder.HasMany( x => x.Recipes )
           .WithOne( x => x.Author );

        builder.HasMany( x => x.Favourites )
           .WithMany( x => x.FavouritedBy );
    }
}

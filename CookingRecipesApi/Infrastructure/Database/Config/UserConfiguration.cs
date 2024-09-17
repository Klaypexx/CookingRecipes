using Domain.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const int _usernameMaxLenght = 25;

    public void Configure( EntityTypeBuilder<User> builder )
    {
        builder.HasKey( x => x.Id );

        builder.Property( a => a.Id );

        builder.Property( x => x.Name )
            .IsRequired( true );

        builder.Property( x => x.UserName )
            .HasMaxLength( _usernameMaxLenght )
            .IsRequired( true );

        builder.Property( x => x.Description );

        builder.Property( x => x.Password )
            .IsRequired( true );

        builder.Property( a => a.RefreshToken )
            .HasDefaultValue( "" )
            .IsRequired( true );

        builder.Property( a => a.RefreshTokenExpiryTime )
            .IsRequired( true )
            .HasDefaultValueSql( "GETDATE()" );

        builder.HasIndex( x => x.UserName )
            .IsUnique();

        builder.HasMany( x => x.Recipes )
           .WithOne( x => x.Author )
           .HasForeignKey( x => x.AuthorId );

        builder.HasMany( u => u.FavouriteRecipes )
            .WithOne( fr => fr.User )
            .HasForeignKey( fr => fr.UserId );

        builder.HasMany( u => u.Likes )
            .WithOne( fr => fr.User )
            .HasForeignKey( fr => fr.UserId );
    }
}

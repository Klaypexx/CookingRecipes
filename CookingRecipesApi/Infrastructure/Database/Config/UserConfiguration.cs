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

        builder.Property( a => a.Id )
            .HasColumnName( "id_user" );

        builder.Property( x => x.Name )
            .HasColumnName( "name" )
            .IsRequired( true );

        builder.Property( x => x.UserName )
            .HasColumnName( "username" )
            .IsRequired( true );


        builder.Property( x => x.Description )
           .HasColumnName( "description" )
           .IsRequired( false );


        builder.Property( x => x.Password )
            .HasColumnName( "password" )
            .IsRequired( true );

        builder.Property( a => a.RefreshToken )
            .HasColumnName( "refresh_token" )
            .HasDefaultValue( "" )
            .IsRequired( true );

        builder.Property( a => a.RefreshTokenExpiryTime )
            .HasColumnName( "refresh_token_expiry_time" )
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

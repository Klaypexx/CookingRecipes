using Domain.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Config;
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("user_id")
            .ValueGeneratedNever();

        builder.Property(a => a.Name)
            .HasColumnName("name")
            .IsRequired(true);

        builder.Property(a => a.UserName)
            .HasColumnName("username")
            .IsRequired(true);

        builder.Property(a => a.Password)
            .HasColumnName("password")
            .IsRequired(true);

        builder.Property(a => a.AvatarPath)
           .HasColumnName("avatar")
           .IsRequired(false);

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.HasMany(u => u.Recipes)
           .WithOne(a => a.User);

        builder.HasMany(u => u.Favourites)
           .WithMany(a => a.FavouritedBy);
    }
}

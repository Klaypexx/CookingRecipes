﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Auth.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("refresh_token_expiry_time")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.FavouriteRecipe", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("id_recipe");

                    b.HasKey("UserId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("favourite_recipe", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_ingredient");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Product")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("product");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("id_recipe");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("ingredient", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Like", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("id_recipe");

                    b.HasKey("UserId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("like", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_recipe");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("id_author");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<int>("CookingTime")
                        .HasColumnType("int")
                        .HasColumnName("time");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("Portion")
                        .HasColumnType("int")
                        .HasColumnName("portion");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("recipe", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.RecipeTag", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("id_recipe");

                    b.Property<int>("TagId")
                        .HasColumnType("int")
                        .HasColumnName("id_tag");

                    b.HasKey("RecipeId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("recipe_tag", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_step");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("id_recipe");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("step", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_tag");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("Domain.Recipes.Entities.FavouriteRecipe", b =>
                {
                    b.HasOne("Domain.Recipes.Entities.Recipe", "Recipe")
                        .WithMany("FavouriteRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Auth.Entities.User", "User")
                        .WithMany("FavouriteRecipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Ingredient", b =>
                {
                    b.HasOne("Domain.Recipes.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Like", b =>
                {
                    b.HasOne("Domain.Recipes.Entities.Recipe", "Recipe")
                        .WithMany("Likes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Auth.Entities.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Recipe", b =>
                {
                    b.HasOne("Domain.Auth.Entities.User", "Author")
                        .WithMany("Recipes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.RecipeTag", b =>
                {
                    b.HasOne("Domain.Recipes.Entities.Recipe", "Recipe")
                        .WithMany("Tags")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Recipes.Entities.Tag", "Tag")
                        .WithMany("Recipes")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Step", b =>
                {
                    b.HasOne("Domain.Recipes.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Domain.Auth.Entities.User", b =>
                {
                    b.Navigation("FavouriteRecipes");

                    b.Navigation("Likes");

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Recipe", b =>
                {
                    b.Navigation("FavouriteRecipes");

                    b.Navigation("Ingredients");

                    b.Navigation("Likes");

                    b.Navigation("Steps");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Domain.Recipes.Entities.Tag", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}

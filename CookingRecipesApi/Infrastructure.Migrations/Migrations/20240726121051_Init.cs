﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id_tag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.id_tag);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    id_recipe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false),
                    portion = table.Column<int>(type: "int", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_author = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.id_recipe);
                    table.ForeignKey(
                        name: "FK_recipe_user_id_author",
                        column: x => x.id_author,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favourite_recipe",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_recipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourite_recipe", x => new { x.id_user, x.id_recipe });
                    table.ForeignKey(
                        name: "FK_favourite_recipe_recipe_id_recipe",
                        column: x => x.id_recipe,
                        principalTable: "recipe",
                        principalColumn: "id_recipe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_favourite_recipe_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    id_ingredient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_recipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.id_ingredient);
                    table.ForeignKey(
                        name: "FK_ingredient_recipe_id_recipe",
                        column: x => x.id_recipe,
                        principalTable: "recipe",
                        principalColumn: "id_recipe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "like",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_recipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_like", x => new { x.id_user, x.id_recipe });
                    table.ForeignKey(
                        name: "FK_like_recipe_id_recipe",
                        column: x => x.id_recipe,
                        principalTable: "recipe",
                        principalColumn: "id_recipe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_like_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recipe_tag",
                columns: table => new
                {
                    id_recipe = table.Column<int>(type: "int", nullable: false),
                    id_tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_tag", x => new { x.id_recipe, x.id_tag });
                    table.ForeignKey(
                        name: "FK_recipe_tag_recipe_id_recipe",
                        column: x => x.id_recipe,
                        principalTable: "recipe",
                        principalColumn: "id_recipe",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipe_tag_tag_id_tag",
                        column: x => x.id_tag,
                        principalTable: "tag",
                        principalColumn: "id_tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "step",
                columns: table => new
                {
                    id_step = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    step_number = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_recipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_step", x => x.id_step);
                    table.ForeignKey(
                        name: "FK_step_recipe_id_recipe",
                        column: x => x.id_recipe,
                        principalTable: "recipe",
                        principalColumn: "id_recipe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_favourite_recipe_id_recipe",
                table: "favourite_recipe",
                column: "id_recipe");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_id_recipe",
                table: "ingredient",
                column: "id_recipe");

            migrationBuilder.CreateIndex(
                name: "IX_like_id_recipe",
                table: "like",
                column: "id_recipe");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_id_author",
                table: "recipe",
                column: "id_author");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_tag_id_tag",
                table: "recipe_tag",
                column: "id_tag");

            migrationBuilder.CreateIndex(
                name: "IX_step_id_recipe",
                table: "step",
                column: "id_recipe");

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                table: "user",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favourite_recipe");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropTable(
                name: "like");

            migrationBuilder.DropTable(
                name: "recipe_tag");

            migrationBuilder.DropTable(
                name: "step");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "recipe");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

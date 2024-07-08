using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    portion = table.Column<int>(type: "int", nullable: true),
                    like = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.recipe_id);
                    table.ForeignKey(
                        name: "FK_recipe_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    ingredient_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "FK_ingredient_recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipe",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_recipe_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "recipe",
                        principalColumn: "recipe_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeUser",
                columns: table => new
                {
                    FavouritedById = table.Column<int>(type: "int", nullable: false),
                    FavouritesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeUser", x => new { x.FavouritedById, x.FavouritesId });
                    table.ForeignKey(
                        name: "FK_RecipeUser_recipe_FavouritesId",
                        column: x => x.FavouritesId,
                        principalTable: "recipe",
                        principalColumn: "recipe_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeUser_user_FavouritedById",
                        column: x => x.FavouritedById,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "step",
                columns: table => new
                {
                    step_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_step", x => x.step_id);
                    table.ForeignKey(
                        name: "FK_step_recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipe",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_RecipeId",
                table: "ingredient",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_UserId",
                table: "recipe",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagsId",
                table: "RecipeTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeUser_FavouritesId",
                table: "RecipeUser",
                column: "FavouritesId");

            migrationBuilder.CreateIndex(
                name: "IX_step_RecipeId",
                table: "step",
                column: "RecipeId");

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
                name: "ingredient");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "RecipeUser");

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

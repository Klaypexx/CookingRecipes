using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class CamelCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourite_recipe_recipe_id_recipe",
                table: "favourite_recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_favourite_recipe_user_id_user",
                table: "favourite_recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_recipe_id_recipe",
                table: "ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_like_recipe_id_recipe",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "FK_like_user_id_user",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_user_id_author",
                table: "recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_tag_recipe_id_recipe",
                table: "recipe_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_tag_tag_id_tag",
                table: "recipe_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_step_recipe_id_recipe",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tag",
                table: "tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_step",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_tag",
                table: "recipe_tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe",
                table: "recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_like",
                table: "like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredient",
                table: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favourite_recipe",
                table: "favourite_recipe");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "user",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "user",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_user_username",
                table: "user",
                newName: "ix_user_user_name");

            migrationBuilder.RenameColumn(
                name: "id_tag",
                table: "tag",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id_recipe",
                table: "step",
                newName: "recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_step",
                table: "step",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_step_id_recipe",
                table: "step",
                newName: "ix_step_recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_tag",
                table: "recipe_tag",
                newName: "tag_id");

            migrationBuilder.RenameColumn(
                name: "id_recipe",
                table: "recipe_tag",
                newName: "recipe_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_tag_id_tag",
                table: "recipe_tag",
                newName: "ix_recipe_tag_tag_id");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "recipe",
                newName: "cooking_time");

            migrationBuilder.RenameColumn(
                name: "id_author",
                table: "recipe",
                newName: "author_id");

            migrationBuilder.RenameColumn(
                name: "id_recipe",
                table: "recipe",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_id_author",
                table: "recipe",
                newName: "ix_recipe_author_id");

            migrationBuilder.RenameColumn(
                name: "id_recipe",
                table: "like",
                newName: "recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "like",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_like_id_recipe",
                table: "like",
                newName: "ix_like_recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_recipe",
                table: "ingredient",
                newName: "recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_ingredient",
                table: "ingredient",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_id_recipe",
                table: "ingredient",
                newName: "ix_ingredient_recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_recipe",
                table: "favourite_recipe",
                newName: "recipe_id");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "favourite_recipe",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_favourite_recipe_id_recipe",
                table: "favourite_recipe",
                newName: "ix_favourite_recipe_recipe_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tag",
                table: "tag",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_step",
                table: "step",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_recipe_tag",
                table: "recipe_tag",
                columns: new[] { "recipe_id", "tag_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_recipe",
                table: "recipe",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_like",
                table: "like",
                columns: new[] { "user_id", "recipe_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_ingredient",
                table: "ingredient",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_favourite_recipe",
                table: "favourite_recipe",
                columns: new[] { "user_id", "recipe_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_favourite_recipe_recipe_recipe_id",
                table: "favourite_recipe",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_favourite_recipe_user_user_id",
                table: "favourite_recipe",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ingredient_recipe_recipe_id",
                table: "ingredient",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_like_recipe_recipe_id",
                table: "like",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_like_user_user_id",
                table: "like",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_recipe_user_author_id",
                table: "recipe",
                column: "author_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recipe_tag_recipe_recipe_id",
                table: "recipe_tag",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recipe_tag_tag_tag_id",
                table: "recipe_tag",
                column: "tag_id",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_step_recipe_recipe_id",
                table: "step",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_favourite_recipe_recipe_recipe_id",
                table: "favourite_recipe");

            migrationBuilder.DropForeignKey(
                name: "fk_favourite_recipe_user_user_id",
                table: "favourite_recipe");

            migrationBuilder.DropForeignKey(
                name: "fk_ingredient_recipe_recipe_id",
                table: "ingredient");

            migrationBuilder.DropForeignKey(
                name: "fk_like_recipe_recipe_id",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "fk_like_user_user_id",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "fk_recipe_user_author_id",
                table: "recipe");

            migrationBuilder.DropForeignKey(
                name: "fk_recipe_tag_recipe_recipe_id",
                table: "recipe_tag");

            migrationBuilder.DropForeignKey(
                name: "fk_recipe_tag_tag_tag_id",
                table: "recipe_tag");

            migrationBuilder.DropForeignKey(
                name: "fk_step_recipe_recipe_id",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tag",
                table: "tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_step",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "pk_recipe_tag",
                table: "recipe_tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_recipe",
                table: "recipe");

            migrationBuilder.DropPrimaryKey(
                name: "pk_like",
                table: "like");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ingredient",
                table: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "pk_favourite_recipe",
                table: "favourite_recipe");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "ix_user_user_name",
                table: "user",
                newName: "IX_user_username");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tag",
                newName: "id_tag");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "step",
                newName: "id_recipe");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "step",
                newName: "id_step");

            migrationBuilder.RenameIndex(
                name: "ix_step_recipe_id",
                table: "step",
                newName: "IX_step_id_recipe");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "recipe_tag",
                newName: "id_tag");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "recipe_tag",
                newName: "id_recipe");

            migrationBuilder.RenameIndex(
                name: "ix_recipe_tag_tag_id",
                table: "recipe_tag",
                newName: "IX_recipe_tag_id_tag");

            migrationBuilder.RenameColumn(
                name: "cooking_time",
                table: "recipe",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "recipe",
                newName: "id_author");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "recipe",
                newName: "id_recipe");

            migrationBuilder.RenameIndex(
                name: "ix_recipe_author_id",
                table: "recipe",
                newName: "IX_recipe_id_author");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "like",
                newName: "id_recipe");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "like",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "ix_like_recipe_id",
                table: "like",
                newName: "IX_like_id_recipe");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "ingredient",
                newName: "id_recipe");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ingredient",
                newName: "id_ingredient");

            migrationBuilder.RenameIndex(
                name: "ix_ingredient_recipe_id",
                table: "ingredient",
                newName: "IX_ingredient_id_recipe");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "favourite_recipe",
                newName: "id_recipe");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "favourite_recipe",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "ix_favourite_recipe_recipe_id",
                table: "favourite_recipe",
                newName: "IX_favourite_recipe_id_recipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id_user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tag",
                table: "tag",
                column: "id_tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_step",
                table: "step",
                column: "id_step");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_tag",
                table: "recipe_tag",
                columns: new[] { "id_recipe", "id_tag" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe",
                table: "recipe",
                column: "id_recipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_like",
                table: "like",
                columns: new[] { "id_user", "id_recipe" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredient",
                table: "ingredient",
                column: "id_ingredient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favourite_recipe",
                table: "favourite_recipe",
                columns: new[] { "id_user", "id_recipe" });

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_recipe_recipe_id_recipe",
                table: "favourite_recipe",
                column: "id_recipe",
                principalTable: "recipe",
                principalColumn: "id_recipe",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_favourite_recipe_user_id_user",
                table: "favourite_recipe",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_recipe_id_recipe",
                table: "ingredient",
                column: "id_recipe",
                principalTable: "recipe",
                principalColumn: "id_recipe",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_like_recipe_id_recipe",
                table: "like",
                column: "id_recipe",
                principalTable: "recipe",
                principalColumn: "id_recipe",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_like_user_id_user",
                table: "like",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_user_id_author",
                table: "recipe",
                column: "id_author",
                principalTable: "user",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_tag_recipe_id_recipe",
                table: "recipe_tag",
                column: "id_recipe",
                principalTable: "recipe",
                principalColumn: "id_recipe",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_tag_tag_id_tag",
                table: "recipe_tag",
                column: "id_tag",
                principalTable: "tag",
                principalColumn: "id_tag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_step_recipe_id_recipe",
                table: "step",
                column: "id_recipe",
                principalTable: "recipe",
                principalColumn: "id_recipe",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

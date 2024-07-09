using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TimeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_user_UserId",
                table: "recipe");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "recipe",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_UserId",
                table: "recipe",
                newName: "IX_recipe_AuthorId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "time",
                table: "recipe",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_user_AuthorId",
                table: "recipe",
                column: "AuthorId",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_user_AuthorId",
                table: "recipe");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "recipe",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_AuthorId",
                table: "recipe",
                newName: "IX_recipe_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "time",
                table: "recipe",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_user_UserId",
                table: "recipe",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}

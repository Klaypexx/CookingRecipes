using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "like",
                table: "recipe");

            migrationBuilder.CreateTable(
                name: "like",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_like", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_like_Id",
                table: "recipe",
                column: "Id",
                principalTable: "like",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_like_Id",
                table: "recipe");

            migrationBuilder.DropTable(
                name: "like");

            migrationBuilder.AddColumn<int>(
                name: "like",
                table: "recipe",
                type: "int",
                nullable: true);
        }
    }
}

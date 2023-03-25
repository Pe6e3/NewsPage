using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPage.Migrations
{
    /// <inheritdoc />
    public partial class MigrationSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ThemeName",
                table: "Themes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Themes",
                newName: "ThemeId");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "ThemeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Themes",
                newName: "ThemeName");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "Themes",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }
    }
}

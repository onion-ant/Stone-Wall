using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Popularity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "ItemsCatalog");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ItemsCatalog",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ItemsCatalog",
                keyColumn: "Title",
                keyValue: null,
                column: "Title",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ItemsCatalog",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Popularity",
                table: "ItemsCatalog",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class Fix_New_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ItemsCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ItemsCatalog");
        }
    }
}

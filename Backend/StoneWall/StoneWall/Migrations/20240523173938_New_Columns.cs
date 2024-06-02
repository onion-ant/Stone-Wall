using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class New_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ItemsCatalog_Streamings",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "expiresSoon",
                table: "ItemsCatalog_Streamings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ItemsCatalog_Streamings");

            migrationBuilder.DropColumn(
                name: "expiresSoon",
                table: "ItemsCatalog_Streamings");
        }
    }
}

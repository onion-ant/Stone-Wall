using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class Fix_ItemGenre_relation_part2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenreItem",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    ItemsTmdbId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreItem", x => new { x.GenresId, x.ItemsTmdbId });
                    table.ForeignKey(
                        name: "FK_GenreItem_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreItem_Items_ItemsTmdbId",
                        column: x => x.ItemsTmdbId,
                        principalTable: "Items",
                        principalColumn: "TmdbId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItem_ItemsTmdbId",
                table: "GenreItem",
                column: "ItemsTmdbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreItem");
        }
    }
}

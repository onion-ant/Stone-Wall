using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class Name_Changing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Streaming_Services_StreamingServicesId",
                table: "Addons");

            migrationBuilder.DropTable(
                name: "GenreItem");

            migrationBuilder.DropTable(
                name: "Item_Streaming");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Streaming_Services");

            migrationBuilder.RenameColumn(
                name: "StreamingServicesId",
                table: "Addons",
                newName: "StreamingId");

            migrationBuilder.RenameIndex(
                name: "IX_Addons_StreamingServicesId",
                table: "Addons",
                newName: "IX_Addons_StreamingId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Genres",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemsCatalog",
                columns: table => new
                {
                    TmdbId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalTitle = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Popularity = table.Column<double>(type: "double", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCatalog", x => x.TmdbId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Streamings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomePage = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streamings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GenreItemCatalog",
                columns: table => new
                {
                    GenresId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemsTmdbId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreItemCatalog", x => new { x.GenresId, x.ItemsTmdbId });
                    table.ForeignKey(
                        name: "FK_GenreItemCatalog_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreItemCatalog_ItemsCatalog_ItemsTmdbId",
                        column: x => x.ItemsTmdbId,
                        principalTable: "ItemsCatalog",
                        principalColumn: "TmdbId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemsCatalog_Streamings",
                columns: table => new
                {
                    TmdbId = table.Column<int>(type: "int", nullable: false),
                    StreamingId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCatalog_Streamings", x => new { x.TmdbId, x.StreamingId });
                    table.ForeignKey(
                        name: "FK_ItemsCatalog_Streamings_ItemsCatalog_TmdbId",
                        column: x => x.TmdbId,
                        principalTable: "ItemsCatalog",
                        principalColumn: "TmdbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsCatalog_Streamings_Streamings_StreamingId",
                        column: x => x.StreamingId,
                        principalTable: "Streamings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItemCatalog_ItemsTmdbId",
                table: "GenreItemCatalog",
                column: "ItemsTmdbId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCatalog_Streamings_StreamingId",
                table: "ItemsCatalog_Streamings",
                column: "StreamingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Streamings_StreamingId",
                table: "Addons",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Streamings_StreamingId",
                table: "Addons");

            migrationBuilder.DropTable(
                name: "GenreItemCatalog");

            migrationBuilder.DropTable(
                name: "ItemsCatalog_Streamings");

            migrationBuilder.DropTable(
                name: "ItemsCatalog");

            migrationBuilder.DropTable(
                name: "Streamings");

            migrationBuilder.RenameColumn(
                name: "StreamingId",
                table: "Addons",
                newName: "StreamingServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_Addons_StreamingId",
                table: "Addons",
                newName: "IX_Addons_StreamingServicesId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    TmdbId = table.Column<int>(type: "int", nullable: false),
                    OriginalTitle = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Popularity = table.Column<double>(type: "double", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.TmdbId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Streaming_Services",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomePage = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streaming_Services", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "Item_Streaming",
                columns: table => new
                {
                    TmdbId = table.Column<int>(type: "int", nullable: false),
                    StreamingId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Link = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Streaming", x => new { x.TmdbId, x.StreamingId });
                    table.ForeignKey(
                        name: "FK_Item_Streaming_Items_TmdbId",
                        column: x => x.TmdbId,
                        principalTable: "Items",
                        principalColumn: "TmdbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Streaming_Streaming_Services_StreamingId",
                        column: x => x.StreamingId,
                        principalTable: "Streaming_Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItem_ItemsTmdbId",
                table: "GenreItem",
                column: "ItemsTmdbId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Streaming_StreamingId",
                table: "Item_Streaming",
                column: "StreamingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Streaming_Services_StreamingServicesId",
                table: "Addons",
                column: "StreamingServicesId",
                principalTable: "Streaming_Services",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class fix_user_genres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Streaming_Streaming_Service_StreamingId",
                table: "Item_Streaming");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Streaming_Service",
                table: "Streaming_Service");

            migrationBuilder.RenameTable(
                name: "Streaming_Service",
                newName: "Streaming_Services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Streaming_Services",
                table: "Streaming_Services",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User_Streaming",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StreamingId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Plan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Streaming", x => new { x.UserEmail, x.StreamingId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "itemGenre",
                columns: table => new
                {
                    TmdbId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemGenre", x => new { x.TmdbId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_itemGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemGenre_Items_TmdbId",
                        column: x => x.TmdbId,
                        principalTable: "Items",
                        principalColumn: "TmdbId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_itemGenre_GenreId",
                table: "itemGenre",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Streaming_Streaming_Services_StreamingId",
                table: "Item_Streaming",
                column: "StreamingId",
                principalTable: "Streaming_Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Streaming_Streaming_Services_StreamingId",
                table: "Item_Streaming");

            migrationBuilder.DropTable(
                name: "itemGenre");

            migrationBuilder.DropTable(
                name: "User_Streaming");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Streaming_Services",
                table: "Streaming_Services");

            migrationBuilder.RenameTable(
                name: "Streaming_Services",
                newName: "Streaming_Service");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Streaming_Service",
                table: "Streaming_Service",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Streaming_Streaming_Service_StreamingId",
                table: "Item_Streaming",
                column: "StreamingId",
                principalTable: "Streaming_Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

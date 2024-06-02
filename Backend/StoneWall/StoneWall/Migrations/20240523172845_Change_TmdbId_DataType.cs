using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class Change_TmdbId_DataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreItemCatalog_ItemsCatalog_ItemsTmdbId",
                table: "GenreItemCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCatalog_Streamings_ItemsCatalog_TmdbId",
                table: "ItemsCatalog_Streamings");

            migrationBuilder.AlterColumn<string>(
                name: "TmdbId",
                table: "ItemsCatalog",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ItemsCatalog",
                keyColumn: "Title",
                keyValue: null,
                column: "Title",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TmdbId",
                table: "ItemsCatalog_Streamings",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ItemsCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);


            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ItemsCatalog",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ItemsCatalog",
                keyColumn: "OriginalTitle",
                keyValue: null,
                column: "OriginalTitle",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalTitle",
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

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ItemsTmdbId",
                table: "GenreItemCatalog",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreItemCatalog_ItemsCatalog_ItemsTmdbId",
                table: "GenreItemCatalog",
                column: "ItemsTmdbId",
                principalTable: "ItemsCatalog",
                principalColumn: "TmdbId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCatalog_Streamings_ItemsCatalog_TmdbId",
                table: "ItemsCatalog_Streamings",
                column: "TmdbId",
                principalTable: "ItemsCatalog",
                principalColumn: "TmdbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TmdbId",
                table: "ItemsCatalog_Streamings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ItemsCatalog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<string>(
                name: "OriginalTitle",
                table: "ItemsCatalog",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "TmdbId",
                table: "ItemsCatalog",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ItemsTmdbId",
                table: "GenreItemCatalog",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

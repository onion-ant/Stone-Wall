using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneWall.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Nullable_propreties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Streamings_StreamingId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCatalog_Streamings_ItemsCatalog_TmdbId",
                table: "ItemsCatalog_Streamings");

            migrationBuilder.RenameColumn(
                name: "TmdbId",
                table: "ItemsCatalog_Streamings",
                newName: "ItemCatalogTmdbId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "User_Streaming",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.UpdateData(
                table: "Addons",
                keyColumn: "StreamingId",
                keyValue: null,
                column: "StreamingId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "StreamingId",
                table: "Addons",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Addons",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Addons",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Addons",
                keyColumn: "HomePage",
                keyValue: null,
                column: "HomePage",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "HomePage",
                table: "Addons",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Streamings_StreamingId",
                table: "Addons",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCatalog_Streamings_ItemsCatalog_ItemCatalogTmdbId",
                table: "ItemsCatalog_Streamings",
                column: "ItemCatalogTmdbId",
                principalTable: "ItemsCatalog",
                principalColumn: "TmdbId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Streamings_StreamingId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCatalog_Streamings_ItemsCatalog_ItemCatalogTmdbId",
                table: "ItemsCatalog_Streamings");

            migrationBuilder.RenameColumn(
                name: "ItemCatalogTmdbId",
                table: "ItemsCatalog_Streamings",
                newName: "TmdbId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "User_Streaming",
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
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StreamingId",
                table: "Addons",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Addons",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "HomePage",
                table: "Addons",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Streamings_StreamingId",
                table: "Addons",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCatalog_Streamings_ItemsCatalog_TmdbId",
                table: "ItemsCatalog_Streamings",
                column: "TmdbId",
                principalTable: "ItemsCatalog",
                principalColumn: "TmdbId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

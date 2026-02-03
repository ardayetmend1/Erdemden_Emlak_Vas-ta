using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class base65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ListingImages",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Base64Data",
                table: "ListingImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ListingImages",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "ListingImages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Data",
                table: "ListingImages");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ListingImages");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "ListingImages");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ListingImages",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}

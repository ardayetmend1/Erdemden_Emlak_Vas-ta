using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class expertmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpertiseReportContentType",
                table: "Listings",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ExpertiseReportData",
                table: "Listings",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpertiseReportFileName",
                table: "Listings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpertiseReportContentType",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ExpertiseReportData",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ExpertiseReportFileName",
                table: "Listings");
        }
    }
}

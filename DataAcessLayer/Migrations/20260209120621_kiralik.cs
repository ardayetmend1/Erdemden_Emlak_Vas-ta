using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class kiralik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "RealEstates",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListingType",
                table: "RealEstates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyRent",
                table: "RealEstates",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "ListingType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "MonthlyRent",
                table: "RealEstates");
        }
    }
}

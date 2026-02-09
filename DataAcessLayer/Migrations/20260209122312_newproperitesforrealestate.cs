using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class newproperitesforrealestate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAirConditioning",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAlarm",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBalcony",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBuiltInKitchen",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasCableTv",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasCellar",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasCoveredParking",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFireplace",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGarden",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGenerator",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasInternet",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasNaturalGas",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPool",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSatellite",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSecurity",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSteelDoor",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasStorageRoom",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasTerrace",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasUnderfloorHeating",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasVideoIntercom",
                table: "RealEstates",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAirConditioning",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasAlarm",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasBalcony",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasBuiltInKitchen",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasCableTv",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasCellar",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasCoveredParking",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasFireplace",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasGarden",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasGenerator",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasInternet",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasNaturalGas",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasPool",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasSatellite",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasSecurity",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasSteelDoor",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasStorageRoom",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasTerrace",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasUnderfloorHeating",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasVideoIntercom",
                table: "RealEstates");
        }
    }
}

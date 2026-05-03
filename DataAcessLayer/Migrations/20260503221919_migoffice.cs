using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migoffice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockNumber",
                table: "RealEstates",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingCondition",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingType",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CeilingHeight",
                table: "RealEstates",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeedStatus",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FloorAreaRatio",
                table: "RealEstates",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FrontWidth",
                table: "RealEstates",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrossArea",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBasement",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasCameraSystem",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasColdStorage",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricityConnection",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFireSystem",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasLoadingDock",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasMezzanine",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasShowcase",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasShutter",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasThreePhasePower",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasWaterSource",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeatingType",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightLimit",
                table: "RealEstates",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreditEligible",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExchangeable",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRoadAccessible",
                table: "RealEstates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NetArea",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParcelNumber",
                table: "RealEstates",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SheetNumber",
                table: "RealEstates",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsageStatus",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoningStatus",
                table: "RealEstates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "HousingTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockNumber",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "BuildingCondition",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "BuildingType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "CeilingHeight",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "DeedStatus",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "FloorAreaRatio",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "FrontWidth",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "GrossArea",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasBasement",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasCameraSystem",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasColdStorage",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasElectricityConnection",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasFireSystem",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasLoadingDock",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasMezzanine",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasShowcase",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasShutter",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasThreePhasePower",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasWaterSource",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HeatingType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HeightLimit",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "IsCreditEligible",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "IsExchangeable",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "IsRoadAccessible",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "NetArea",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "ParcelNumber",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "SheetNumber",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "UsageStatus",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "ZoningStatus",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "HousingTypes");
        }
    }
}

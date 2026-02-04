using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class vehiclefeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasABS",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAUX",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAirConditioning",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAirbag",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBlindSpotWarning",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBluetooth",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCarPlay",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCentralLock",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCruiseControl",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDigitalAC",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasESP",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricMirrors",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricWindows",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasImmobilizer",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasIsofix",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLaneAssist",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLeatherSeats",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNavigation",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParkingSensor",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPremiumSound",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRearCamera",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRearEntertainment",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSeatHeating",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasStartStop",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSteeringWheelHeating",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSunroof",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTouchScreen",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUSB",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasABS",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAUX",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAirConditioning",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAirbag",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasBlindSpotWarning",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasBluetooth",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasCarPlay",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasCentralLock",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasCruiseControl",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasDigitalAC",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasESP",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasElectricMirrors",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasElectricWindows",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasImmobilizer",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasIsofix",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasLaneAssist",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasLeatherSeats",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasNavigation",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasParkingSensor",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasPremiumSound",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasRearCamera",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasRearEntertainment",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasSeatHeating",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasStartStop",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasSteeringWheelHeating",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasSunroof",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasTouchScreen",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasUSB",
                table: "Vehicles");
        }
    }
}

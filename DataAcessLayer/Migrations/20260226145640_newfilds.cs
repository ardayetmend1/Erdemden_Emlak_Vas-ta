using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class newfilds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAEB",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAdaptiveCruiseControl",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAdaptiveLights",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAndroidAuto",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasArmoredVehicle",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasArmrest",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAutoDimmingMirror",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBAS",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasChildLock",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCooledGlovebox",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDistronic",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDriverAirbag",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricFoldMirrors",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricSeats",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFabricSeats",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFatigueDetection",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFootTrunkOpener",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFrontCamera",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFrontParkSensor",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFunctionalSteering",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHardtop",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHeadUpDisplay",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHeatedMirrors",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHeatedSteering",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHillAssist",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHydraulicSteering",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasKeylessEntry",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMemoryMirrors",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMemorySeats",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNightVision",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPanoramicRoof",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParkAssist",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPassengerAirbag",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRearParkSensor",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSeatCooling",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSmartTrunk",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSpeedLimiter",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasThirdRowSeats",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTowBar",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTripComputer",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAEB",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAdaptiveCruiseControl",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAdaptiveLights",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAndroidAuto",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasArmoredVehicle",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasArmrest",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasAutoDimmingMirror",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasBAS",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasChildLock",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasCooledGlovebox",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasDistronic",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasDriverAirbag",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasElectricFoldMirrors",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasElectricSeats",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasFabricSeats",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasFatigueDetection",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasFootTrunkOpener",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasFrontCamera",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasFrontParkSensor",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasFunctionalSteering",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasHardtop",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasHeadUpDisplay",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasHeatedMirrors",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasHeatedSteering",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasHillAssist",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasHydraulicSteering",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasKeylessEntry",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasMemoryMirrors",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasMemorySeats",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasNightVision",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasPanoramicRoof",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasParkAssist",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasPassengerAirbag",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasRearParkSensor",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasSeatCooling",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasSmartTrunk",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasSpeedLimiter",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasThirdRowSeats",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasTowBar",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasTripComputer",
                table: "Vehicles");
        }
    }
}

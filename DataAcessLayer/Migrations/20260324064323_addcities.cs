using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addcities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NeighborhoodId",
                table: "Listings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Listings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_NeighborhoodId",
                table: "Listings",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_DistrictId",
                table: "Neighborhoods",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Neighborhoods_NeighborhoodId",
                table: "Listings",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Neighborhoods_NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Neighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_Listings_NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Listings");
        }
    }
}

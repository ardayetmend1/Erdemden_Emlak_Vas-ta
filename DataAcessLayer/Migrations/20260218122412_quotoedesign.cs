using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class quotoedesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OfferDate",
                table: "QuoteRequests",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferMaxPrice",
                table: "QuoteRequests",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferMinPrice",
                table: "QuoteRequests",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "QuoteRequests",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "QuoteRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_Status",
                table: "QuoteRequests",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_Status",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "OfferDate",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "OfferMaxPrice",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "OfferMinPrice",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "QuoteRequests");
        }
    }
}

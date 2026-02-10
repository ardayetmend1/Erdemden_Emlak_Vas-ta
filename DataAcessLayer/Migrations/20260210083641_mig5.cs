using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_Date",
                table: "QuoteRequests",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_Email",
                table: "QuoteRequests",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_IsRead",
                table: "QuoteRequests",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_Category",
                table: "Listings",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ListingDate",
                table: "Listings",
                column: "ListingDate");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_Status",
                table: "Listings",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_Date",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_Email",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_IsRead",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_Listings_Category",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_ListingDate",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_Status",
                table: "Listings");
        }
    }
}

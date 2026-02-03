using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class bodytypeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BodyTypeId",
                table: "Models",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BodyTypeId",
                table: "Models",
                column: "BodyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_BodyTypes_BodyTypeId",
                table: "Models",
                column: "BodyTypeId",
                principalTable: "BodyTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_BodyTypes_BodyTypeId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BodyTypeId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BodyTypeId",
                table: "Models");
        }
    }
}

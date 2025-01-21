using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iFredApps.Cloud.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixLicenseUserIdMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Licenses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_UserId1",
                table: "Licenses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Users_UserId1",
                table: "Licenses",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Users_UserId1",
                table: "Licenses");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_UserId1",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Licenses");
        }
    }
}

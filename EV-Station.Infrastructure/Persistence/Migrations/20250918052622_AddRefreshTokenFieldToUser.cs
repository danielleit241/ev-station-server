using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_Station.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverLicense_Users_UserId",
                table: "DriverLicense");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityCard_Users_UserId",
                table: "IdentityCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityCard",
                table: "IdentityCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverLicense",
                table: "DriverLicense");

            migrationBuilder.RenameTable(
                name: "IdentityCard",
                newName: "IdentityCards");

            migrationBuilder.RenameTable(
                name: "DriverLicense",
                newName: "DriverLicenses");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityCard_UserId",
                table: "IdentityCards",
                newName: "IX_IdentityCards_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverLicense_UserId",
                table: "DriverLicenses",
                newName: "IX_DriverLicenses_UserId");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "IdentityCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityCards",
                table: "IdentityCards",
                column: "CardNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverLicenses",
                table: "DriverLicenses",
                column: "LicenseNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverLicenses_Users_UserId",
                table: "DriverLicenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityCards_Users_UserId",
                table: "IdentityCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverLicenses_Users_UserId",
                table: "DriverLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityCards_Users_UserId",
                table: "IdentityCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityCards",
                table: "IdentityCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverLicenses",
                table: "DriverLicenses");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "IdentityCards");

            migrationBuilder.RenameTable(
                name: "IdentityCards",
                newName: "IdentityCard");

            migrationBuilder.RenameTable(
                name: "DriverLicenses",
                newName: "DriverLicense");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityCards_UserId",
                table: "IdentityCard",
                newName: "IX_IdentityCard_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverLicenses_UserId",
                table: "DriverLicense",
                newName: "IX_DriverLicense_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityCard",
                table: "IdentityCard",
                column: "CardNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverLicense",
                table: "DriverLicense",
                column: "LicenseNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverLicense_Users_UserId",
                table: "DriverLicense",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityCard_Users_UserId",
                table: "IdentityCard",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

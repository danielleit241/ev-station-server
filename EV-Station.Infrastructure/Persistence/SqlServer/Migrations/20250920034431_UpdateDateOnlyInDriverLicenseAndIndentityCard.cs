using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_Station.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateOnlyInDriverLicenseAndIndentityCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "IdentityCards",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "DriverLicenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "DriverLicenses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "IdentityCards",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}

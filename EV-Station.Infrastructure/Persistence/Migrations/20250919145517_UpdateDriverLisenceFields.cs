using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_Station.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDriverLisenceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "IdentityCards",
                newName: "DayOfExpiry");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpiresDate",
                table: "DriverLicenses",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BeginingDate",
                table: "DriverLicenses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassificationOfMotorVehicles",
                table: "DriverLicenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginingDate",
                table: "DriverLicenses");

            migrationBuilder.DropColumn(
                name: "ClassificationOfMotorVehicles",
                table: "DriverLicenses");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "IdentityCards",
                newName: "DayOfExpiry");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresDate",
                table: "DriverLicenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}

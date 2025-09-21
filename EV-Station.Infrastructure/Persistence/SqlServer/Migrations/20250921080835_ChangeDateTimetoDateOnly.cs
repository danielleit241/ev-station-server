using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_Station.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTimetoDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "ExpiryDate",
            //    table: "IdentityCards",
            //    newName: "DayOfExpiry");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "DriverLicenses",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayOfExpiry",
                table: "IdentityCards",
                newName: "ExpiryDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "DriverLicenses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}

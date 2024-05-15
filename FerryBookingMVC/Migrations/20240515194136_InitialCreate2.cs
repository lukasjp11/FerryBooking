using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerryBookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ferries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MaxCars", "MaxGuests", "Name", "PricePerCar", "PricePerGuest" },
                values: new object[] { 400, 980, "MOLSLINJEN (Express 4)", 249m, 149m });

            migrationBuilder.UpdateData(
                table: "Ferries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaxCars", "MaxGuests", "Name" },
                values: new object[] { 50, 100, "Standard Ferry" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ferries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MaxCars", "MaxGuests", "Name", "PricePerCar", "PricePerGuest" },
                values: new object[] { 100, 50, "Ferry 1", 197m, 99m });

            migrationBuilder.UpdateData(
                table: "Ferries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaxCars", "MaxGuests", "Name" },
                values: new object[] { 120, 60, "Ferry 2" });
        }
    }
}

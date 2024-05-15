using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FerryBookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGuestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ferries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCars = table.Column<int>(type: "int", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: false),
                    PricePerGuest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerCar = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FerryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Ferries_FerryId",
                        column: x => x.FerryId,
                        principalTable: "Ferries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    FerryId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Guests_Ferries_FerryId",
                        column: x => x.FerryId,
                        principalTable: "Ferries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Ferries",
                columns: new[] { "Id", "MaxCars", "MaxGuests", "Name", "PricePerCar", "PricePerGuest" },
                values: new object[,]
                {
                    { 1, 400, 980, "MOLSLINJEN (Express 4)", 249m, 149m },
                    { 2, 50, 100, "Standard Ferry", 197m, 99m }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "FerryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "CarId", "FerryId", "Gender", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, true, "Alice Smith" },
                    { 2, null, 1, false, "Bob Johnson" },
                    { 3, null, 1, false, "Charlie Brown" },
                    { 4, null, 1, true, "Diana Prince" },
                    { 5, null, 2, true, "Eve Davis" },
                    { 6, null, 2, false, "Frank Miller" },
                    { 7, null, 2, true, "Grace Lee" },
                    { 8, null, 2, false, "Hank Green" },
                    { 9, null, 1, false, "Isaac Newton" },
                    { 10, null, 1, true, "Marie Curie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FerryId",
                table: "Cars",
                column: "FerryId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_CarId",
                table: "Guests",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_FerryId",
                table: "Guests",
                column: "FerryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Ferries");
        }
    }
}

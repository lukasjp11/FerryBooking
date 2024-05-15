using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FerryBookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
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
                    MaxCars = table.Column<int>(type: "int", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: false),
                    GuestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    CarId = table.Column<int>(type: "int", nullable: false),
                    FerryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guests_Ferries_FerryId",
                        column: x => x.FerryId,
                        principalTable: "Ferries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Ferries",
                columns: new[] { "Id", "CarPrice", "GuestPrice", "MaxCars", "MaxGuests" },
                values: new object[,]
                {
                    { 1, 197m, 99m, 100, 50 },
                    { 2, 197m, 99m, 120, 60 }
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
                    { 1, 1, 1, "Female", "Alice Smith" },
                    { 2, 1, 1, "Male", "Bob Johnson" },
                    { 3, 2, 1, "Male", "Charlie Brown" },
                    { 4, 2, 1, "Female", "Diana Prince" },
                    { 5, 3, 2, "Female", "Eve Davis" },
                    { 6, 3, 2, "Male", "Frank Miller" },
                    { 7, 4, 2, "Female", "Grace Lee" },
                    { 8, 4, 2, "Male", "Hank Green" }
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

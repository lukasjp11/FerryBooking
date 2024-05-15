using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FerryBookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Length = table.Column<int>(type: "int", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: false)
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
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    FerryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Ferries_FerryId",
                        column: x => x.FerryId,
                        principalTable: "Ferries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    FerryId = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Ferries",
                columns: new[] { "Id", "Length", "MaxGuests" },
                values: new object[,]
                {
                    { 1, 10, 40 },
                    { 2, 20, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverId",
                table: "Cars",
                column: "DriverId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Guests_DriverId",
                table: "Cars",
                column: "DriverId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Ferries_FerryId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Ferries_FerryId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Guests_DriverId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Ferries");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}

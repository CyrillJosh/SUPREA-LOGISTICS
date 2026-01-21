using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUPREA_LOGISTICS.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverInChargeId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverInChargeId",
                table: "Vehicles",
                column: "DriverInChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Drivers_DriverInChargeId",
                table: "Vehicles",
                column: "DriverInChargeId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Drivers_DriverInChargeId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_DriverInChargeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DriverInChargeId",
                table: "Vehicles");
        }
    }
}

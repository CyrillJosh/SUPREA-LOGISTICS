using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUPREA_LOGISTICS.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOdometerFromVehicleLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdometerStart",
                table: "VehicleLogs");

            migrationBuilder.DropColumn(
                name: "OdometerEnd",
                table: "VehicleLogs");
        }

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.AddColumn<int>(
        //        name: "OdometerStart",
        //        table: "VehicleLogs",
        //        type: "int",
        //        nullable: true);

        //    migrationBuilder.AddColumn<int>(
        //        name: "OdometerEnd",
        //        table: "VehicleLogs",
        //        type: "int",
        //        nullable: true);
        //}
    }
}

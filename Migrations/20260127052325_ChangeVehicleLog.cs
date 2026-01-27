using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUPREA_LOGISTICS.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVehicleLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "VehicleLogs");

            migrationBuilder.DropColumn(
                name: "LogDate",
                table: "VehicleLogs");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "VehicleLogs");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VehicleLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "VehicleLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VehicleLogs");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "VehicleLogs");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "VehicleLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LogDate",
                table: "VehicleLogs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "VehicleLogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}

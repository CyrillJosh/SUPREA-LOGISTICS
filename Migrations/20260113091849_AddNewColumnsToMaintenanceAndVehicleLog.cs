using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUPREA_LOGISTICS.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsToMaintenanceAndVehicleLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add Remarks column to MaintenanceLogs
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "MaintenanceLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

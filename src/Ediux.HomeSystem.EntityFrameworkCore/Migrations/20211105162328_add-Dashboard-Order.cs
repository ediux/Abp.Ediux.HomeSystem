using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class addDashboardOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "App_DashboardWidgets",
                type: "int",
                nullable: false,
                defaultValue: -1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "App_DashboardWidgets");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class addDashboardDisplayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "App_DashboardWidgets",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "App_DashboardWidgets");
        }
    }
}

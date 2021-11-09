using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class addDashboardWidgetUserAssigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SettingName",
                table: "App_DashboardWidgets");

            migrationBuilder.CreateTable(
                name: "App_DashboardWidgetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DashboardWidgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSettings = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_DashboardWidgetUsers", x => new { x.Id, x.DashboardWidgetId });
                    table.ForeignKey(
                        name: "FK_App_DashboardWidgetUsers_App_DashboardWidgets_DashboardWidgetId",
                        column: x => x.DashboardWidgetId,
                        principalTable: "App_DashboardWidgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_DashboardWidgetUsers_DashboardWidgetId",
                table: "App_DashboardWidgetUsers",
                column: "DashboardWidgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_DashboardWidgetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SettingName",
                table: "App_DashboardWidgets",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}

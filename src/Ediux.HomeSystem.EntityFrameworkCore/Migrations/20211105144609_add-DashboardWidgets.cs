using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class addDashboardWidgets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App_DashboardWidgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HasOption = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowUserSetting = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AllowMulti = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SettingName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PermissionName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_DashboardWidgets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_DashboardWidgets");
        }
    }
}

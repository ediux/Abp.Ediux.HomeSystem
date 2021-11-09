using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class addComponentsRegistrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "App_DashboardWidgets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "App_ComponentsRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SettingName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PermissionName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HasOption = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowUserSetting = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_ComponentsRegistration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_ComponentsRegistration");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "App_DashboardWidgets");
        }
    }
}

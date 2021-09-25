using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class AddPluginRegisterupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppAbpPlugins",
                table: "AppAbpPlugins");

            migrationBuilder.RenameTable(
                name: "AppAbpPlugins",
                newName: "AppPlugins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPlugins",
                table: "AppPlugins",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPlugins",
                table: "AppPlugins");

            migrationBuilder.RenameTable(
                name: "AppPlugins",
                newName: "AppAbpPlugins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppAbpPlugins",
                table: "AppAbpPlugins",
                column: "Id");
        }
    }
}

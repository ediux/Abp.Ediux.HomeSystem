using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class AddPluginRegisterupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpPlugins",
                table: "AbpPlugins");

            migrationBuilder.RenameTable(
                name: "AbpPlugins",
                newName: "AppAbpPlugins");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "AppAbpPlugins",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "載入路徑",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppAbpPlugins",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "模組名稱",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppAbpPlugins",
                table: "AppAbpPlugins",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppAbpPlugins",
                table: "AppAbpPlugins");

            migrationBuilder.RenameTable(
                name: "AppAbpPlugins",
                newName: "AbpPlugins");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "AbpPlugins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldComment: "載入路徑");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AbpPlugins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "模組名稱");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpPlugins",
                table: "AbpPlugins",
                column: "Id");
        }
    }
}

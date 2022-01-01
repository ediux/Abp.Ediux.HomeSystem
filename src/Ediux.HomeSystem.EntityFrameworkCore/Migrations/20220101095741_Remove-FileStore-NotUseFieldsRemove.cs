using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class RemoveFileStoreNotUseFieldsRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InRecycle",
                table: "App_File_Store");

            migrationBuilder.DropColumn(
                name: "IsCrypto",
                table: "App_File_Store");

            migrationBuilder.DropColumn(
                name: "SMBFullPath",
                table: "App_File_Store");

            migrationBuilder.DropColumn(
                name: "SMBLoginId",
                table: "App_File_Store");

            migrationBuilder.DropColumn(
                name: "SMBPassword",
                table: "App_File_Store");

            migrationBuilder.DropColumn(
                name: "StorageInSMB",
                table: "App_File_Store");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InRecycle",
                table: "App_File_Store",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCrypto",
                table: "App_File_Store",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SMBFullPath",
                table: "App_File_Store",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMBLoginId",
                table: "App_File_Store",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMBPassword",
                table: "App_File_Store",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "StorageInSMB",
                table: "App_File_Store",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

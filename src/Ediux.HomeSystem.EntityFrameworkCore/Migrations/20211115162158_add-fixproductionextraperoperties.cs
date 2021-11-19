using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class addfixproductionextraperoperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedField1",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedField2",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedField3",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedField4",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedField5",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedFieldName1",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedFieldName2",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedFieldName3",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedFieldName4",
                table: "App_UserPasswordStore");

            migrationBuilder.DropColumn(
                name: "ReservedFieldName5",
                table: "App_UserPasswordStore");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "App_ProductKeys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "App_ProductKeys");

            migrationBuilder.AddColumn<string>(
                name: "ReservedField1",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedField2",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedField3",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedField4",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedField5",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedFieldName1",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedFieldName2",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedFieldName3",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedFieldName4",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedFieldName5",
                table: "App_UserPasswordStore",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}

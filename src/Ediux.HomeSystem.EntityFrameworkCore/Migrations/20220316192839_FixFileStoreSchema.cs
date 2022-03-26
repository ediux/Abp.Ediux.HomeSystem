using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class FixFileStoreSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "HomeSystemFiles");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "HomeSystemFiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HomeSystemFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 16, 19, 28, 38, 546, DateTimeKind.Utc).AddTicks(3224),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 14, 5, 32, 17, 630, DateTimeKind.Utc).AddTicks(25));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 14, 5, 32, 17, 630, DateTimeKind.Utc).AddTicks(25),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 16, 19, 28, 38, 546, DateTimeKind.Utc).AddTicks(3224));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "HomeSystemFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "HomeSystemFiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HomeSystemFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

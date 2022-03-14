using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class FixUserPasswordStoreDefine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "HomeSystemUserPasswordStores",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 14, 5, 32, 17, 630, DateTimeKind.Utc).AddTicks(25),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 9, 18, 34, 42, 984, DateTimeKind.Utc).AddTicks(6863));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "HomeSystemUserPasswordStores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 9, 18, 34, 42, 984, DateTimeKind.Utc).AddTicks(6863),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 14, 5, 32, 17, 630, DateTimeKind.Utc).AddTicks(25));
        }
    }
}

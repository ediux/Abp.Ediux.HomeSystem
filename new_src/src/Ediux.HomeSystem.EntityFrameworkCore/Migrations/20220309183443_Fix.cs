using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 9, 18, 34, 42, 984, DateTimeKind.Utc).AddTicks(6863),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 7, 18, 27, 28, 216, DateTimeKind.Utc).AddTicks(843));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "HomeSystemProductKeyStores",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "HomeSystemProductKeyStores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 7, 18, 27, 28, 216, DateTimeKind.Utc).AddTicks(843),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 9, 18, 34, 42, 984, DateTimeKind.Utc).AddTicks(6863));
        }
    }
}

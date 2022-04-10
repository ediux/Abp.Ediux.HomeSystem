using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ediux.HomeSystem.Migrations
{
    public partial class cmskit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 27, 14, 29, 18, 182, DateTimeKind.Utc).AddTicks(1498),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 25, 20, 39, 12, 745, DateTimeKind.Utc).AddTicks(3560));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 25, 20, 39, 12, 745, DateTimeKind.Utc).AddTicks(3560),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 27, 14, 29, 18, 182, DateTimeKind.Utc).AddTicks(1498));
        }
    }
}

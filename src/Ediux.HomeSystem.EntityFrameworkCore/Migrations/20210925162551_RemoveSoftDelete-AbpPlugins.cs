using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class RemoveSoftDeleteAbpPlugins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppPlugins");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppPlugins");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppPlugins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppPlugins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppPlugins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppPlugins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

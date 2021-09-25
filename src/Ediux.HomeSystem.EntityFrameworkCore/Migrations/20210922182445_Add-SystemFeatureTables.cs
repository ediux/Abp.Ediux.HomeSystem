using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class AddSystemFeatureTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App_Calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    groupId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    allDay = table.Column<bool>(type: "bit", nullable: false),
                    t_start = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    t_end = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    classNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    editable = table.Column<bool>(type: "bit", nullable: false),
                    startEditable = table.Column<bool>(type: "bit", nullable: false),
                    durationEditable = table.Column<bool>(type: "bit", nullable: false),
                    resourceEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsAdded = table.Column<bool>(type: "bit", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Calendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_MIMETypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MIME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RefenceExtName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_MIMETypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_ProductKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProductKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Shared = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_ProductKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_UserPasswordStore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Site = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    SiteName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Account = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsHistory = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservedField1 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedField2 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedField3 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedField4 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedField5 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedFieldName1 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedFieldName2 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedFieldName3 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedFieldName4 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReservedFieldName5 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_UserPasswordStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_File_Store",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MIMETypeId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    IsCrypto = table.Column<bool>(type: "bit", nullable: false),
                    InRecycle = table.Column<bool>(type: "bit", nullable: false),
                    OriginFullPath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    StorageInSMB = table.Column<bool>(type: "bit", nullable: false),
                    SMBFullPath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    SMBLoginId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SMBPassword = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_File_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_App_File_Store_App_MIMETypes_MIMETypeId",
                        column: x => x.MIMETypeId,
                        principalTable: "App_MIMETypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_File_Store_MIMETypeId",
                table: "App_File_Store",
                column: "MIMETypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_Calendars");

            migrationBuilder.DropTable(
                name: "App_File_Store");

            migrationBuilder.DropTable(
                name: "App_ProductKeys");

            migrationBuilder.DropTable(
                name: "App_UserPasswordStore");

            migrationBuilder.DropTable(
                name: "App_MIMETypes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Migrations
{
    public partial class _202203089236_AddSystemFeatureData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeSystemClassifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemClassifications_HomeSystemClassifications_ParentClassificationId",
                        column: x => x.ParentClassificationId,
                        principalTable: "HomeSystemClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemDashboardWidgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    HasOption = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowMulti = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemDashboardWidgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemMIMETypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, defaultValue: "application/octet-stream"),
                    RefenceExtName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemMIMETypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemProductKeyStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProductKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Shared = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemProductKeyStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemSystemMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: false),
                    IsEMail = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsPush = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsCC = table.Column<bool>(type: "bit", nullable: true),
                    IsBCC = table.Column<bool>(type: "bit", nullable: true),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 3, 7, 18, 27, 28, 216, DateTimeKind.Utc).AddTicks(843)),
                    ReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActionCallbackURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    RefenceMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReply = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_HomeSystemSystemMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemSystemMessages_AbpUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSystemSystemMessages_HomeSystemSystemMessages_RefenceMessageId",
                        column: x => x.RefenceMessageId,
                        principalTable: "HomeSystemSystemMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemUserPasswordStores",
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
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemUserPasswordStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemDashboardWidgetUsers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DashboardWidgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemDashboardWidgetUsers", x => new { x.UserId, x.DashboardWidgetId });
                    table.ForeignKey(
                        name: "FK_HomeSystemDashboardWidgetUsers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSystemDashboardWidgetUsers_HomeSystemDashboardWidgets_DashboardWidgetId",
                        column: x => x.DashboardWidgetId,
                        principalTable: "HomeSystemDashboardWidgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    MIMETypeId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    FileClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BlobContainerName = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
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
                    table.PrimaryKey("PK_HomeSystemFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemFiles_HomeSystemClassifications_FileClassificationId",
                        column: x => x.FileClassificationId,
                        principalTable: "HomeSystemClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeSystemFiles_HomeSystemMIMETypes_MIMETypeId",
                        column: x => x.MIMETypeId,
                        principalTable: "HomeSystemMIMETypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemCalendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefenceEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsAllDay = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SystemMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_HomeSystemCalendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemCalendars_HomeSystemCalendars_RefenceEventId",
                        column: x => x.RefenceEventId,
                        principalTable: "HomeSystemCalendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeSystemCalendars_HomeSystemSystemMessages_SystemMessageId",
                        column: x => x.SystemMessageId,
                        principalTable: "HomeSystemSystemMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemAttachFiles",
                columns: table => new
                {
                    SystemMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemAttachFiles", x => new { x.SystemMessageId, x.FileStoreId });
                    table.ForeignKey(
                        name: "FK_HomeSystemAttachFiles_HomeSystemFiles_FileStoreId",
                        column: x => x.FileStoreId,
                        principalTable: "HomeSystemFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSystemAttachFiles_HomeSystemSystemMessages_SystemMessageId",
                        column: x => x.SystemMessageId,
                        principalTable: "HomeSystemSystemMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemPlugins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    RefFileStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Disabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemPlugins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemPlugins_HomeSystemFiles_RefFileStoreId",
                        column: x => x.RefFileStoreId,
                        principalTable: "HomeSystemFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemAttachFiles_FileStoreId",
                table: "HomeSystemAttachFiles",
                column: "FileStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemCalendars_RefenceEventId",
                table: "HomeSystemCalendars",
                column: "RefenceEventId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemCalendars_SystemMessageId",
                table: "HomeSystemCalendars",
                column: "SystemMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemClassifications_ParentClassificationId",
                table: "HomeSystemClassifications",
                column: "ParentClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemDashboardWidgetUsers_DashboardWidgetId",
                table: "HomeSystemDashboardWidgetUsers",
                column: "DashboardWidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemFiles_FileClassificationId",
                table: "HomeSystemFiles",
                column: "FileClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemFiles_MIMETypeId",
                table: "HomeSystemFiles",
                column: "MIMETypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemPlugins_RefFileStoreId",
                table: "HomeSystemPlugins",
                column: "RefFileStoreId",
                unique: true,
                filter: "[RefFileStoreId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemSystemMessages_FromUserId",
                table: "HomeSystemSystemMessages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemSystemMessages_RefenceMessageId",
                table: "HomeSystemSystemMessages",
                column: "RefenceMessageId",
                unique: true,
                filter: "[RefenceMessageId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeSystemAttachFiles");

            migrationBuilder.DropTable(
                name: "HomeSystemCalendars");

            migrationBuilder.DropTable(
                name: "HomeSystemDashboardWidgetUsers");

            migrationBuilder.DropTable(
                name: "HomeSystemPlugins");

            migrationBuilder.DropTable(
                name: "HomeSystemProductKeyStores");

            migrationBuilder.DropTable(
                name: "HomeSystemUserPasswordStores");

            migrationBuilder.DropTable(
                name: "HomeSystemSystemMessages");

            migrationBuilder.DropTable(
                name: "HomeSystemDashboardWidgets");

            migrationBuilder.DropTable(
                name: "HomeSystemFiles");

            migrationBuilder.DropTable(
                name: "HomeSystemClassifications");

            migrationBuilder.DropTable(
                name: "HomeSystemMIMETypes");
        }
    }
}

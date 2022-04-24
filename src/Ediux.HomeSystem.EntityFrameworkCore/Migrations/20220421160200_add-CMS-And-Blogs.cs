using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ediux.HomeSystem.Migrations
{
    public partial class addCMSAndBlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 21, 16, 2, 0, 104, DateTimeKind.Utc).AddTicks(7619),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 27, 14, 29, 18, 182, DateTimeKind.Utc).AddTicks(1498));

            migrationBuilder.CreateTable(
                name: "HomeSystemBlogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_HomeSystemBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemBlogs_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RepliedCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemComments_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeSystemComments_HomeSystemComments_RepliedCommentId",
                        column: x => x.RepliedCommentId,
                        principalTable: "HomeSystemComments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemConsortiums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsPrivateEnterprise = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupRepresentative = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    LogoFileRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_HomeSystemConsortiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemConsortiums_HomeSystemFiles_LogoFileRef",
                        column: x => x.LogoFileRef,
                        principalTable: "HomeSystemFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_HomeSystemPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemPages_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StarCount = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemRatings_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
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
                    table.PrimaryKey("PK_HomeSystemTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemTags_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemBlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageMediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_HomeSystemBlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemBlogPosts_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeSystemBlogPosts_AbpUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSystemBlogPosts_HomeSystemBlogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "HomeSystemBlogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSystemBlogPosts_HomeSystemFiles_CoverImageMediaId",
                        column: x => x.CoverImageMediaId,
                        principalTable: "HomeSystemFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemMenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CssClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemMenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSystemMenuItems_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeSystemMenuItems_HomeSystemMenuItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "HomeSystemMenuItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeSystemMenuItems_HomeSystemPages_PageId",
                        column: x => x.PageId,
                        principalTable: "HomeSystemPages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeSystemEntityTags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSystemEntityTags", x => new { x.TagId, x.EntityId });
                    table.ForeignKey(
                        name: "FK_HomeSystemEntityTags_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeSystemEntityTags_HomeSystemTags_TagId",
                        column: x => x.TagId,
                        principalTable: "HomeSystemTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemBlogPosts_AuthorId",
                table: "HomeSystemBlogPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemBlogPosts_BlogId",
                table: "HomeSystemBlogPosts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemBlogPosts_CoverImageMediaId",
                table: "HomeSystemBlogPosts",
                column: "CoverImageMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemBlogPosts_TenantId",
                table: "HomeSystemBlogPosts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemBlogs_TenantId",
                table: "HomeSystemBlogs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemComments_RepliedCommentId",
                table: "HomeSystemComments",
                column: "RepliedCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemComments_TenantId",
                table: "HomeSystemComments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemConsortiums_LogoFileRef",
                table: "HomeSystemConsortiums",
                column: "LogoFileRef");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemEntityTags_TenantId",
                table: "HomeSystemEntityTags",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemMenuItems_PageId",
                table: "HomeSystemMenuItems",
                column: "PageId",
                unique: true,
                filter: "[PageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemMenuItems_ParentId",
                table: "HomeSystemMenuItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemMenuItems_TenantId",
                table: "HomeSystemMenuItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemPages_TenantId",
                table: "HomeSystemPages",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemRatings_TenantId",
                table: "HomeSystemRatings",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSystemTags_TenantId",
                table: "HomeSystemTags",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeSystemBlogPosts");

            migrationBuilder.DropTable(
                name: "HomeSystemComments");

            migrationBuilder.DropTable(
                name: "HomeSystemConsortiums");

            migrationBuilder.DropTable(
                name: "HomeSystemEntityTags");

            migrationBuilder.DropTable(
                name: "HomeSystemMenuItems");

            migrationBuilder.DropTable(
                name: "HomeSystemRatings");

            migrationBuilder.DropTable(
                name: "HomeSystemBlogs");

            migrationBuilder.DropTable(
                name: "HomeSystemTags");

            migrationBuilder.DropTable(
                name: "HomeSystemPages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "HomeSystemSystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 27, 14, 29, 18, 182, DateTimeKind.Utc).AddTicks(1498),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 21, 16, 2, 0, 104, DateTimeKind.Utc).AddTicks(7619));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ediux.HomeSystem.Plugins.HololivePages.Migrations
{
    public partial class addmoduleInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HololiveGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupRepresentative = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    LogoFileRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HololiveCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsHQ = table.Column<bool>(type: "bit", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyRepresentative = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CEO = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    LogoFileRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HololiveCompanies_HololiveGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "HololiveGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HololiveBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    LogoFileRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HololiveBranches_HololiveCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "HololiveCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HololiveDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HololiveDepartments_HololiveBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "HololiveBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HololiveMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SureName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JanpaneseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChineseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DebutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    height = table.Column<int>(type: "int", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Live2DDesigner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Live3DDesigner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChannelFeatures = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    VTOperatorExpertise = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: true),
                    Subscriptions = table.Column<int>(type: "int", nullable: false),
                    DepartmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HololiveMembers_HololiveDepartments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "HololiveDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HololiveMemberEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveMemberEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HololiveMemberEvents_HololiveMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "HololiveMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HololivePhotosRefence",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololivePhotosRefence", x => new { x.MemberId, x.FileId });
                    table.ForeignKey(
                        name: "FK_HololivePhotosRefence_HololiveMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "HololiveMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HololiveYTuberVideoRefence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HololiveYTuberVideoRefence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HololiveYTuberVideoRefence_HololiveMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "HololiveMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HololiveBranches_CompanyId",
                table: "HololiveBranches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_HololiveCompanies_GroupId",
                table: "HololiveCompanies",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HololiveDepartments_BranchId",
                table: "HololiveDepartments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HololiveMemberEvents_MemberId",
                table: "HololiveMemberEvents",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_HololiveMembers_DepartmentsId",
                table: "HololiveMembers",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_HololiveYTuberVideoRefence_MemberId",
                table: "HololiveYTuberVideoRefence",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HololiveMemberEvents");

            migrationBuilder.DropTable(
                name: "HololivePhotosRefence");

            migrationBuilder.DropTable(
                name: "HololiveYTuberVideoRefence");

            migrationBuilder.DropTable(
                name: "HololiveMembers");

            migrationBuilder.DropTable(
                name: "HololiveDepartments");

            migrationBuilder.DropTable(
                name: "HololiveBranches");

            migrationBuilder.DropTable(
                name: "HololiveCompanies");

            migrationBuilder.DropTable(
                name: "HololiveGroups");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.DeveloperId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet",
                columns: table => new
                {
                    TimeSheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet", x => x.TimeSheetId);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developer",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_DeveloperId",
                table: "TimeSheet",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_ProjectId",
                table: "TimeSheet",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheet");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}

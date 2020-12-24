using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_developer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperProject",
                columns: table => new
                {
                    DeveloperId = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: false),
                    DeveloperId1 = table.Column<int>(nullable: true),
                    ProjectId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperProject", x => new { x.DeveloperId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_DeveloperProject_tb_developer_DeveloperId1",
                        column: x => x.DeveloperId1,
                        principalTable: "tb_developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeveloperProject_tb_project_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "tb_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_hour",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Begin = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    DeveloperId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_hour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_hour_tb_developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "tb_developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_hour_tb_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tb_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperProject_DeveloperId1",
                table: "DeveloperProject",
                column: "DeveloperId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperProject_ProjectId1",
                table: "DeveloperProject",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_tb_hour_DeveloperId",
                table: "tb_hour",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_hour_ProjectId",
                table: "tb_hour",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperProject");

            migrationBuilder.DropTable(
                name: "tb_hour");

            migrationBuilder.DropTable(
                name: "tb_developer");

            migrationBuilder.DropTable(
                name: "tb_project");
        }
    }
}

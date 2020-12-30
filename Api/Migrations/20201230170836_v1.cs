using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteDotnet.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => new { x.Id, x.CPF });
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperProject",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    DeveloperId1 = table.Column<int>(type: "int", nullable: true),
                    DeveloperCPF = table.Column<string>(type: "nvarchar(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperProject", x => new { x.DeveloperId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_DeveloperProject_Developer_DeveloperId1_DeveloperCPF",
                        columns: x => new { x.DeveloperId1, x.DeveloperCPF },
                        principalTable: "Developer",
                        principalColumns: new[] { "Id", "CPF" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeveloperProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitialDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    DeveloperId1 = table.Column<int>(type: "int", nullable: true),
                    DeveloperCPF = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_Developer_DeveloperId1_DeveloperCPF",
                        columns: x => new { x.DeveloperId1, x.DeveloperCPF },
                        principalTable: "Developer",
                        principalColumns: new[] { "Id", "CPF" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entry_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Developer",
                columns: new[] { "CPF", "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { "12345678900", 1, "abc@gmail.com", "Leonardo", "1234" },
                    { "12345678901", 2, "abc1@gmail.com", "Maria", "1234" },
                    { "12345678902", 3, "abc2@gmail.com", "José", "1234" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Web App" },
                    { 2, "API" },
                    { 3, "Database" }
                });

            migrationBuilder.InsertData(
                table: "DeveloperProject",
                columns: new[] { "DeveloperId", "ProjectId", "DeveloperCPF", "DeveloperId1" },
                values: new object[,]
                {
                    { 1, 1, null, null },
                    { 2, 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "Entry",
                columns: new[] { "Id", "DeveloperCPF", "DeveloperId", "DeveloperId1", "EndDate", "InitialDate", "ProjectId" },
                values: new object[,]
                {
                    { 1, null, 1, null, new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 30, 14, 8, 34, 990, DateTimeKind.Local).AddTicks(6175), 1 },
                    { 2, null, 2, null, new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 30, 14, 8, 35, 2, DateTimeKind.Local).AddTicks(7430), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperProject_DeveloperId1_DeveloperCPF",
                table: "DeveloperProject",
                columns: new[] { "DeveloperId1", "DeveloperCPF" });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperProject_ProjectId",
                table: "DeveloperProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_DeveloperId1_DeveloperCPF",
                table: "Entry",
                columns: new[] { "DeveloperId1", "DeveloperCPF" });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_ProjectId",
                table: "Entry",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperProject");

            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}

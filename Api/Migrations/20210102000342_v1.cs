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
                name: "WorkedHoursRank",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    WorkedHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
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
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    DeveloperId1 = table.Column<int>(type: "int", nullable: true),
                    DeveloperCPF = table.Column<string>(type: "nvarchar(11)", nullable: true)
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
                    { "54011152021", 1, "leonardo@gmail.com", "Leonardo", "1234" },
                    { "83336894000", 2, "abc1@gmail.com", "Maria", "1234" },
                    { "07426071006", 3, "abc2@gmail.com", "José", "1234" },
                    { "01928079008", 4, "abc3@gmail.com", "Leonardo2", "1234" },
                    { "13757262000", 5, "abc4@gmail.com", "Maria2", "1234" },
                    { "70610579045", 6, "abc5@gmail.com", "José2", "1234" },
                    { "05827793086", 7, "abc6@gmail.com", "Leonardo3", "1234" },
                    { "65451565007", 8, "abc7@gmail.com", "Maria3", "1234" },
                    { "40687197058", 9, "abc8@gmail.com", "José3", "1234" },
                    { "28596429000", 10, "abc9@gmail.com", "Leonardo4", "1234" },
                    { "54621768050", 11, "abc10@gmail.com", "Maria4", "1234" },
                    { "55705968019", 12, "abc11@gmail.com", "José4", "1234" }
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
                    { 16, null, 5, null, new DateTime(2021, 1, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 15, null, 4, null, new DateTime(2021, 1, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 14, null, 4, null, new DateTime(2021, 1, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 13, null, 3, null, new DateTime(2021, 1, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, null, 3, null, new DateTime(2021, 1, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, null, 3, null, new DateTime(2021, 1, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 17, null, 5, null, new DateTime(2021, 1, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 10, null, 2, null, new DateTime(2021, 1, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, null, 2, null, new DateTime(2021, 1, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, null, 2, null, new DateTime(2021, 1, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, null, 1, null, new DateTime(2021, 1, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, null, 1, null, new DateTime(2021, 1, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, null, 1, null, new DateTime(2021, 1, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, null, 1, null, new DateTime(2021, 1, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1, null, 1, null, new DateTime(2021, 1, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, null, 2, null, new DateTime(2021, 1, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 18, null, 6, null, new DateTime(2021, 1, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 3 }
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
                name: "WorkedHoursRank");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Migrations
{
    public partial class hours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfHours",
                table: "TimeSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfHours",
                table: "TimeSheet");
        }
    }
}

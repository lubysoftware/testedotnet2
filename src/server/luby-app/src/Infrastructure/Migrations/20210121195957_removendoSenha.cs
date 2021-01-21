using Microsoft.EntityFrameworkCore.Migrations;

namespace luby_app.Infrastructure.Migrations
{
    public partial class removendoSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Desenvolvedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Desenvolvedor",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

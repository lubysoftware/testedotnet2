using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_Teste.Migrations
{
    public partial class inicial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "DevProjeto",
                newName: "DevProjetoid");

            migrationBuilder.CreateIndex(
                name: "IX_DevProjeto_DesenvolvedorId",
                table: "DevProjeto",
                column: "DesenvolvedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DevProjeto_ProjetoId",
                table: "DevProjeto",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DevProjeto_Desenvolvedor_DesenvolvedorId",
                table: "DevProjeto",
                column: "DesenvolvedorId",
                principalTable: "Desenvolvedor",
                principalColumn: "DesenvolvedorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DevProjeto_Projeto_ProjetoId",
                table: "DevProjeto",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "ProjetoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevProjeto_Desenvolvedor_DesenvolvedorId",
                table: "DevProjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_DevProjeto_Projeto_ProjetoId",
                table: "DevProjeto");

            migrationBuilder.DropIndex(
                name: "IX_DevProjeto_DesenvolvedorId",
                table: "DevProjeto");

            migrationBuilder.DropIndex(
                name: "IX_DevProjeto_ProjetoId",
                table: "DevProjeto");

            migrationBuilder.RenameColumn(
                name: "DevProjetoid",
                table: "DevProjeto",
                newName: "id");
        }
    }
}

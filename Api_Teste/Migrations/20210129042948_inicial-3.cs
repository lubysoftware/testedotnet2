using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_Teste.Migrations
{
    public partial class inicial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_DesenvolvedorId",
                table: "Lancamento",
                column: "DesenvolvedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_Desenvolvedor_DesenvolvedorId",
                table: "Lancamento",
                column: "DesenvolvedorId",
                principalTable: "Desenvolvedor",
                principalColumn: "DesenvolvedorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_Desenvolvedor_DesenvolvedorId",
                table: "Lancamento");

            migrationBuilder.DropIndex(
                name: "IX_Lancamento_DesenvolvedorId",
                table: "Lancamento");
        }
    }
}

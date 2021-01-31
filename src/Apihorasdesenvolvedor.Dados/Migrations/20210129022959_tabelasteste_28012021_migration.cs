using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dados.Migrations
{
    public partial class tabelasteste_28012021_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesenvolvedorXProjeto",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AlteradoEm = table.Column<DateTime>(nullable: true),
                    Fk_Desenvolvedor = table.Column<int>(nullable: false),
                    Fk_Projeto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvolvedorXProjeto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AlteradoEm = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TblDesenvolvedor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AlteradoEm = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 40, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 80, nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDesenvolvedor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TblDesenvolvedorXLancamentoHoras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AlteradoEm = table.Column<DateTime>(nullable: true),
                    Fk_Desenvolvedor = table.Column<int>(nullable: false),
                    Fk_Projeto = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDesenvolvedorXLancamentoHoras", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_Nome",
                table: "Projeto",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblDesenvolvedor_Cpf",
                table: "TblDesenvolvedor",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblDesenvolvedor_Email",
                table: "TblDesenvolvedor",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenvolvedorXProjeto");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "TblDesenvolvedor");

            migrationBuilder.DropTable(
                name: "TblDesenvolvedorXLancamentoHoras");
        }
    }
}

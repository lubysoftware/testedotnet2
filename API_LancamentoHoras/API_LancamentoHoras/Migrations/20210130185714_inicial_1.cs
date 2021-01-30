using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_LancamentoHoras.Migrations
{
    public partial class inicial_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desenvolvedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desenvolvedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LancamentoHoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesenvolvedorId = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoHoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LancamentoHoras_Desenvolvedor_DesenvolvedorId",
                        column: x => x.DesenvolvedorId,
                        principalTable: "Desenvolvedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LancamentoHoras_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjetoDesenvolvedor",
                columns: table => new
                {
                    DesenvolvedorId = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoDesenvolvedor", x => new { x.ProjetoId, x.DesenvolvedorId });
                    table.ForeignKey(
                        name: "FK_ProjetoDesenvolvedor_Desenvolvedor_DesenvolvedorId",
                        column: x => x.DesenvolvedorId,
                        principalTable: "Desenvolvedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjetoDesenvolvedor_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Desenvolvedor",
                columns: new[] { "Id", "Cpf", "Nome" },
                values: new object[,]
                {
                    { 1, "15648548545", "Lauro" },
                    { 2, "94851451545", "Roberto" },
                    { 3, "45180084610", "Ronaldo" },
                    { 4, "00451104001", "Rodrigo" },
                    { 5, "74050048122", "Alexandre" }
                });

            migrationBuilder.InsertData(
                table: "Projeto",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Agendamento e Horas" },
                    { 2, "Bar e Mercadinhos" },
                    { 3, "Empresa" }
                });

            migrationBuilder.InsertData(
                table: "LancamentoHoras",
                columns: new[] { "Id", "DataFinal", "DataInicial", "DesenvolvedorId", "ProjetoId" },
                values: new object[,]
                {
                    { 2, new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 5, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 3, new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2 },
                    { 4, new DateTime(2020, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 1, new DateTime(2018, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProjetoDesenvolvedor",
                columns: new[] { "DesenvolvedorId", "ProjetoId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 5, 2 },
                    { 1, 2 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoHoras_DesenvolvedorId",
                table: "LancamentoHoras",
                column: "DesenvolvedorId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoHoras_ProjetoId",
                table: "LancamentoHoras",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoDesenvolvedor_DesenvolvedorId",
                table: "ProjetoDesenvolvedor",
                column: "DesenvolvedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentoHoras");

            migrationBuilder.DropTable(
                name: "ProjetoDesenvolvedor");

            migrationBuilder.DropTable(
                name: "Desenvolvedor");

            migrationBuilder.DropTable(
                name: "Projeto");
        }
    }
}

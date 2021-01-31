using Apihorasdesenvolvedor.Dados.Rastreamento;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Apihorasdesenvolvedor.Dados.Contexto
{
    public class MyContext : DbContext
    {
        public DbSet<DesenvolvedorEntity> Desenvolvedor { get; set; }
        public DbSet<DesenvolvedorXLancamentohorasEntity> DesenvolvedorXLancamentoHoras { get; set; }
        public DbSet<DesenvolvedorXProjetoEntity> DesenvolvedorXProjeto { get; set; }
        public DbSet<ProjetoEntity> Projeto { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DesenvolvedorEntity>(new DesenvolvedorRastreamento().Configure);
            modelBuilder.Entity<DesenvolvedorXLancamentohorasEntity>(new DesenvolvedorXLancamentohorasRastreamento().Configure);
            modelBuilder.Entity<DesenvolvedorXProjetoEntity>(new DesenvolvedorXProjetoRastreamento().Configure);
            modelBuilder.Entity<ProjetoEntity>(new ProjetoRastreamento().Configure);
        }
    }
}

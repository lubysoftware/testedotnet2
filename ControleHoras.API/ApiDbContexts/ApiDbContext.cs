using ControleHoras.API.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleHoras.API.ApiDbContexts
{
    public partial class ApiDbContext : DbContext
    {
        public virtual DbSet<Lancamento> Lancamentos { get; set; }
        public virtual DbSet<Desenvolvedor> Desenvolvedores { get; set; }
        public virtual DbSet<Projeto> Projetos { get; set; }
        public virtual DbSet<DesenvolvedorProjeto> DesenvolvedorProjeto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = App.Configuration.GetConnectionString("Database");
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DesenvolvedorProjeto>()
                        .ToTable("desenvolvedor_projeto")
                        .HasKey(t => new { t.IdDesenvolvedor, t.IdProjeto });

            modelBuilder.Entity<Desenvolvedor>()
                        .ToTable("desenvolvedores")
                        .HasMany(x => x.DesenvolvedorProjeto)
                        .WithOne(q => q.Desenvolvedor)
                        .HasForeignKey(x => x.IdDesenvolvedor);

            modelBuilder.Entity<Projeto>()
                        .ToTable("projetos")
                        .HasMany(x => x.DesenvolvedorProjeto)
                        .WithOne(q => q.Projeto)
                        .HasForeignKey(x => x.IdProjeto);

            modelBuilder.Entity<Lancamento>()
                        .ToTable("lancamentos")
                        .HasOne(x => x.Desenvolvedor)
                        .WithMany(y => y.Lancamentos)
                        .HasForeignKey(w => w.IdDesenvolvedor);
        }
    }
}

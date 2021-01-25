using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Data.Context
{
    public class DesafioDbContext: DbContext
    {


        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options)
        {

        }

        public DbSet<Desenvolvedor> Desenvolvedor { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<ProjetoDesenvolvedor> ProjetosDesenvolvedores { get; set; }
        public DbSet<LancamentoHoras> LancamentoHoras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime))))
                property.SetColumnType("datetime");

            modelBuilder.Entity<Desenvolvedor>().HasData(new Desenvolvedor
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Administrador",
                CPF = "000.000.00-00",
                Usuario = "Admin",
                Senha = "1234"
            });


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCriacao").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
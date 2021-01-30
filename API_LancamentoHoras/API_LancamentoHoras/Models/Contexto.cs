using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_LancamentoHoras.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<LancamentoHoras> LancamentoHoras { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Desenvolvedor> Desenvolvedor { get; set; }
        public DbSet<ProjetoDesenvolvedor> ProjetoDesenvolvedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjetoDesenvolvedor>(entity =>
            {
                entity.HasKey(e => new { e.ProjetoId, e.DesenvolvedorId });
            });
        }
    }
}

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
        public DbSet<DesenvolvedorProjeto> ProjetoDesenvolvedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DesenvolvedorProjeto>(entity =>
            {
                entity.HasKey(e => new { e.ProjetoId, e.DesenvolvedorId }); //Duas chaves compostas (muitos para muitos)
            });

            modelBuilder.Entity<Desenvolvedor>()
                .HasData(new List<Desenvolvedor>(){
                    new Desenvolvedor(1,"Lauro","15648548545"),
                    new Desenvolvedor(2,"Roberto","94851451545"),
                    new Desenvolvedor(3,"Ronaldo","45180084610"),
                    new Desenvolvedor(4,"Rodrigo","00451104001"),
                    new Desenvolvedor(5,"Alexandre","74050048122"),
                });

            modelBuilder.Entity<Projeto>()
                .HasData(new List<Projeto>(){
                    new Projeto(1,"Agendamento e Horas"),
                    new Projeto(2,"Bar e Mercadinhos"),
                    new Projeto(3,"Empresa"),
                });

            modelBuilder.Entity<LancamentoHoras>()
                .HasData(new List<LancamentoHoras>(){
                    new LancamentoHoras(1,DateTime.Parse("2018-03-01"),DateTime.Parse("2018-04-03"),4,3),
                    new LancamentoHoras(2,DateTime.Parse("2019-05-21"),DateTime.Parse("2019-07-01"),2,1),
                    new LancamentoHoras(3,DateTime.Parse("2020-03-17"),DateTime.Parse("2020-07-14"),5,2),
                    new LancamentoHoras(4,DateTime.Parse("2020-08-04"),DateTime.Parse("2020-09-29"),1,2),
                    new LancamentoHoras(5,DateTime.Parse("2021-03-02"),DateTime.Parse("2021-08-20"),3,1),
                });

            modelBuilder.Entity<DesenvolvedorProjeto>()
                .HasData(new List<DesenvolvedorProjeto>(){
                    new DesenvolvedorProjeto(4,3),
                    new DesenvolvedorProjeto(2,1),
                    new DesenvolvedorProjeto(5,2),
                    new DesenvolvedorProjeto(1,2),
                    new DesenvolvedorProjeto(3,1),
                });
        }
    }
}

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
        public DbSet<DesenvolvedorProjeto> DesenvolvedorProjeto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DesenvolvedorProjeto>(entity =>
            {
                entity.HasKey(e => new { e.ProjetoId, e.DesenvolvedorId }); //Duas chaves compostas (muitos para muitos)
            });

            modelBuilder.Entity<Desenvolvedor>()
                .HasData(new List<Desenvolvedor>(){
                    new Desenvolvedor(1,"Lauro","15648548545","Lauro"),
                    new Desenvolvedor(2,"Roberto","94851451545","Roberto"),
                    new Desenvolvedor(3,"Ronaldo","45180084610","Ronaldo"),
                    new Desenvolvedor(4,"Rodrigo","00451104001","Rodrigo"),
                    new Desenvolvedor(5,"Alexandre","74050048122","Alexandre"),
                });

            modelBuilder.Entity<Projeto>()
                .HasData(new List<Projeto>(){
                    new Projeto(1,"Agendamento e Horas"),
                    new Projeto(2,"Bar e Mercadinhos"),
                    new Projeto(3,"Empresa"),
                });

            modelBuilder.Entity<DesenvolvedorProjeto>()
                .HasData(new List<DesenvolvedorProjeto>(){
                    new DesenvolvedorProjeto(4,3),
                    new DesenvolvedorProjeto(2,1),
                    new DesenvolvedorProjeto(5,2),
                    new DesenvolvedorProjeto(1,2),
                    new DesenvolvedorProjeto(3,1),
                    new DesenvolvedorProjeto(1,1),
                });

            DateTime Hoje = DateTime.Now;
            DateTime AntesOntem = Hoje.AddDays(-2);
            DateTime Ontem = Hoje.AddDays(-1);
            DateTime Amanha = Hoje.AddDays(1);
            DateTime DepoisAmanha = Hoje.AddDays(2);

            modelBuilder.Entity<LancamentoHoras>()
                .HasData(new List<LancamentoHoras>(){
                    new LancamentoHoras(1,new DateTime(AntesOntem.Year, AntesOntem.Month, AntesOntem.Day, 13, 25, 50),new DateTime(AntesOntem.Year, AntesOntem.Month, AntesOntem.Day, 14, 50, 0),4,3),
                    new LancamentoHoras(2,new DateTime(AntesOntem.Year, AntesOntem.Month, AntesOntem.Day, 13, 20, 0),new DateTime(AntesOntem.Year, AntesOntem.Month, AntesOntem.Day, 15, 20, 0),5,2),
                    new LancamentoHoras(3,new DateTime(Ontem.Year, Ontem.Month, Ontem.Day, 8, 0, 0),new DateTime(Ontem.Year, Ontem.Month, Ontem.Day, 10, 25, 0),2,1),
                    new LancamentoHoras(4,new DateTime(Hoje.Year, Hoje.Month, Hoje.Day, 14, 30, 0),new DateTime(Hoje.Year, Hoje.Month, Hoje.Day, 18, 50, 0),5,2),
                    new LancamentoHoras(5,new DateTime(Hoje.Year, Hoje.Month, Hoje.Day, 10, 15, 0),new DateTime(Hoje.Year, Hoje.Month, Hoje.Day, 15, 0, 0),1,2),
                    new LancamentoHoras(6,new DateTime(Amanha.Year, Amanha.Month, Amanha.Day, 8, 10, 0),new DateTime(Amanha.Year, Amanha.Month, Amanha.Day, 20, 10, 0),3,1),
                    new LancamentoHoras(7,new DateTime(Amanha.Year, Amanha.Month, Amanha.Day, 18, 10, 0),new DateTime(Amanha.Year, Amanha.Month, Amanha.Day, 20, 10, 0),1,1),
                    new LancamentoHoras(8,new DateTime(DepoisAmanha.Year, DepoisAmanha.Month, DepoisAmanha.Day, 18, 10, 0),new DateTime(DepoisAmanha.Year, DepoisAmanha.Month, DepoisAmanha.Day, 20, 10, 0),1,1),
                    new LancamentoHoras(9,new DateTime(DepoisAmanha.Year, DepoisAmanha.Month, DepoisAmanha.Day, 8, 10, 0),new DateTime(DepoisAmanha.Year, DepoisAmanha.Month, DepoisAmanha.Day, 17, 30, 0),5,2)
                });
        }
    }
}

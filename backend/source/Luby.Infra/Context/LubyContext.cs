using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Luby.Infra.Context
{
    public partial class LubyContext : DbContext
    {
        public LubyContext()
        {
        }

        public LubyContext(DbContextOptions<LubyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Desenvolvedor> Desenvolvedors { get; set; }
        public virtual DbSet<Equipeprojeto> Equipeprojetos { get; set; }
        public virtual DbSet<Lancamento> Lancamentos { get; set; }
        public virtual DbSet<Projeto> Projetos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//                optionsBuilder.UseMySql("server=localhost;database=luby;user=root", Microsoft.EntityFrameworkCore.ServerVersion.FromString("10.1.37-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Desenvolvedor>(entity =>
            {
                entity.ToTable("desenvolvedor");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cargo)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("cargo");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasColumnName("cpf")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("email")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Login)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("login")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nome)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("senha")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Equipeprojeto>(entity =>
            {
                entity.ToTable("equipeprojetos");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdDesenvolvedor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_desenvolvedor");

                entity.Property(e => e.IdProjeto)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_projeto");
            });

            modelBuilder.Entity<Lancamento>(entity =>
            {
                entity.ToTable("lancamento");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DtFim)
                    .HasColumnType("date")
                    .HasColumnName("dt_fim");

                entity.Property(e => e.DtInicio)
                    .HasColumnType("date")
                    .HasColumnName("dt_inicio");

                entity.Property(e => e.IdDesenvolvedor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_desenvolvedor");

                entity.Property(e => e.IdProjeto)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_projeto");
            });

            modelBuilder.Entity<Projeto>(entity =>
            {
                entity.ToTable("projeto");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

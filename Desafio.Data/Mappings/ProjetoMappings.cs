using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class ProjetoMappings : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");


            // 1 : N => Projeto : Lancamentos
            builder.HasMany(p => p.Lancamentos)
                .WithOne(l => l.Projeto)
                .HasForeignKey(l => l.ProjetoId);


            builder.ToTable("Projeto");
        }


    }
}
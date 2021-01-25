using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class DesenvolvedorMapping : IEntityTypeConfiguration<Desenvolvedor>
    {
        public void Configure(EntityTypeBuilder<Desenvolvedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.CPF)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(p => p.Email)
                .IsRequired(false)
                .HasColumnType("varchar(50)");



            // 1 : N => Desenvolvedor : Lancamentos
            builder.HasMany(d => d.Lancamentos)
                .WithOne(l => l.Desenvolvedor)
                .HasForeignKey(l => l.DesenvolvedorId);



            builder.ToTable("Desenvolvedor");
        }


    }
}


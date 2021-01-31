using Apihorasdesenvolvedor.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apihorasdesenvolvedor.Dados.Rastreamento
{
    public class DesenvolvedorRastreamento : IEntityTypeConfiguration<DesenvolvedorEntity>
    {
        public void Configure(EntityTypeBuilder<DesenvolvedorEntity> builder)
        {
            builder.ToTable("TblDesenvolvedor");
            builder.HasKey(x => x.id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Sobrenome).IsRequired().HasMaxLength(80);
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(11);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

        }
    }
}

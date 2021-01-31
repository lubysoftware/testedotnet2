using Apihorasdesenvolvedor.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apihorasdesenvolvedor.Dados.Rastreamento
{
    public class ProjetoRastreamento
    {
        public void Configure(EntityTypeBuilder<ProjetoEntity> builder)
        {
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.Nome).IsUnique();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(30);

        }
    }
}

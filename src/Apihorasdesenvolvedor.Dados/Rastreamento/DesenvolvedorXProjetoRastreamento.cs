using Apihorasdesenvolvedor.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apihorasdesenvolvedor.Dados.Rastreamento
{
    public class DesenvolvedorXProjetoRastreamento
    {
        public void Configure(EntityTypeBuilder<DesenvolvedorXProjetoEntity> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.Fk_Desenvolvedor).IsRequired();
            builder.Property(x => x.Fk_Projeto).IsRequired();
        }

    }
}

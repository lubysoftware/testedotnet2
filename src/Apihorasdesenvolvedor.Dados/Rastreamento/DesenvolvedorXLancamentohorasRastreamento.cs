using Apihorasdesenvolvedor.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apihorasdesenvolvedor.Dados.Rastreamento
{
    public class DesenvolvedorXLancamentohorasRastreamento
    {
        public void Configure(EntityTypeBuilder<DesenvolvedorXLancamentohorasEntity> builder)
        {
            builder.ToTable("TblDesenvolvedorXLancamentoHoras");
            builder.HasKey(x => x.id);
            builder.Property(x => x.Fk_Desenvolvedor).IsRequired();
            builder.Property(x => x.Fk_Projeto).IsRequired();
            builder.Property(x => x.DataInicio).IsRequired();
            builder.Property(x => x.DataFim).IsRequired();

        }
    }
}

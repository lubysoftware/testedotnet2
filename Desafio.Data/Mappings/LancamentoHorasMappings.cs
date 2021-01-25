using Desafio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Data.Mappings
{
    public class LancamentoHorasMappings : IEntityTypeConfiguration<LancamentoHoras>
    {
        public void Configure(EntityTypeBuilder<LancamentoHoras> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(x => x.DataInicio)
                .IsRequired(true)
                .HasColumnType("datetime")
                .HasColumnName("DataInicio");

            builder.Property(x => x.DataFim)
                .IsRequired(true)
                .HasColumnType("datetime")
                .HasColumnName("DataFim");


            builder.ToTable("LancamentoHoras");
        }


    }
}
using luby_app.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace luby_app.Infrastructure.Persistence.Configurations
{
    public class DesenvolvedorConfiguration : IEntityTypeConfiguration<Desenvolvedor>
    {
        public void Configure(EntityTypeBuilder<Desenvolvedor> builder)
        {
            builder.Property(t => t.    Nome)
                .HasMaxLength(200)
                .IsRequired(); 
        }
    }

    public class DesenvolvedorHoraConfiguration : IEntityTypeConfiguration<DesenvolvedorHora>
    {
        public void Configure(EntityTypeBuilder<DesenvolvedorHora> builder)
        {
            builder.Property(t => t.Inicio) 
                .IsRequired();

            builder.Property(t => t.DesenvolvedorId)
               .IsRequired();

            builder.Property(t => t.Fim)
               .IsRequired();

            builder.Ignore(el => el.TotalHoras);
        }
    }
}
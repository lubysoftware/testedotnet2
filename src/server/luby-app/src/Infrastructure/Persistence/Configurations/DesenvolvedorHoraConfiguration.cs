using luby_app.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace luby_app.Infrastructure.Persistence.Configurations
{
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

            builder.HasOne(t => t.Desenvolvedor)
                .WithMany(t => t.DesenvolvedorHora)
                .HasForeignKey(t => t.DesenvolvedorId); 

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
using luby_app.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace luby_app.Infrastructure.Persistence.Configurations
{
    public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        { 
            builder.Property(t => t.Nome)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
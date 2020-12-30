using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Developers;

namespace Tasks.Ifrastructure.Mapping
{
    public class DeveloperMap : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.ToTable("Developers");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.CPF).HasMaxLength(11).IsRequired();
            builder.Property(d => d.Name).HasMaxLength(150).IsRequired();
            builder.Property(d => d.Login).HasMaxLength(150).IsRequired();
            builder.Property(d => d.Password).HasMaxLength(80).IsRequired();

            builder.HasIndex(d => d.Login).IsUnique();
        }
    }
}

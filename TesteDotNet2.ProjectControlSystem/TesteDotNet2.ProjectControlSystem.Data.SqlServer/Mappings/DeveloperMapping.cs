using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Extension;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Mappings
{
    public class DeveloperMapping : EntityTypeConfiguration<Developer>
    {
        public override void Map(EntityTypeBuilder<Developer> builder)
        {
            builder.HasKey(c => c.DeveloperId);

            builder.Property(e => e.CPF)
               .HasColumnType("varchar(11)")
               .IsRequired();

            builder.Property(e => e.Name)
               .HasColumnType("varchar(50)")
               .IsRequired();               

            builder.ToTable("Developer");
        }
    }
}

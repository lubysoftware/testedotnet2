using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Extension;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Mappings
{
    public class ProjectMapping : EntityTypeConfiguration<Project>
    {
        public override void Map(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(c => c.ProjectId);           

            builder.Property(e => e.Name)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.ToTable("Project");
        }
    }
}

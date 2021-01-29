using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Extension;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Mappings
{
    public class TimeSheetMapping : EntityTypeConfiguration<TimeSheet>
    {
        public override void Map(EntityTypeBuilder<TimeSheet> builder)
        {
            builder.HasKey(c => c.TimeSheetId);

            builder.Property(e => e.BeginDate)
               .HasColumnType("datetime")
               .IsRequired();

            builder.Property(e => e.EndDate)
               .HasColumnType("datetime")
               .IsRequired();

            builder.Property(e => e.AmountOfHours)
               .HasColumnType("int")
               .IsRequired();

            builder.HasOne(e => e.Developer)
            .WithMany(o => o.TimeSheets)
            .HasForeignKey(e => e.DeveloperId);

            builder.HasOne(e => e.Project)
            .WithMany(o => o.TimeSheets)
            .HasForeignKey(e => e.ProjectId);

            builder.ToTable("TimeSheet");
        }
    }
}

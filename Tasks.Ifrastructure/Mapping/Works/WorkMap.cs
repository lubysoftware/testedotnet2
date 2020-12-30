using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities.Works;

namespace Tasks.Ifrastructure.Mapping
{
    public class WorkMap : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Works");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.StartTime).IsRequired();
            builder.Property(w => w.EndTime).IsRequired();
            builder.Property(w => w.Comment).HasMaxLength(300).IsRequired();

            builder.HasIndex(w => w.StartTime);
            builder.HasIndex(w => w.EndTime);
        }
    }
}

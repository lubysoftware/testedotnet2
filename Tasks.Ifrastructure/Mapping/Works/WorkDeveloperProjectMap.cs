using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities.Works;

namespace Tasks.Ifrastructure.Mapping.Works
{
    public class WorkDeveloperProjectMap : IEntityTypeConfiguration<WorkDeveloperProject>
    {
        public void Configure(EntityTypeBuilder<WorkDeveloperProject> builder)
        {
            builder.ToTable("WorkDeveloperProjects");

            builder.HasKey(wdp => wdp.Id);

            builder.HasOne(wdp => wdp.DeveloperProject)
                .WithMany(dp => dp.WorkDeveloperProjects)
                .HasForeignKey(wdp => wdp.DeveloperProjectId);

            builder.HasOne(wdp => wdp.Work)
                .WithMany(w => w.WorkDeveloperProjects)
                .HasForeignKey(wdp => wdp.WorkId);

            builder.HasIndex(wdp => new { wdp.DeveloperProjectId, wdp.WorkId }).IsUnique();
        }
    }
}

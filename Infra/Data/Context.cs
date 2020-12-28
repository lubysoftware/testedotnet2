using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace Infra
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<RankModel> Rank { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RankModel>().HasNoKey();
            modelBuilder.Entity<Developer>(entity =>
            {
                entity.HasMany(dev => dev.Hours).WithOne(hour => hour.Developer).HasForeignKey(hour => hour.DeveloperId);
            });
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasMany(dev => dev.Hours).WithOne(hour => hour.Project).HasForeignKey(hour => hour.ProjectId);
            });
            modelBuilder.Entity<DeveloperProject>().HasKey(devprj => new { devprj.DeveloperId, devprj.ProjectId });
        }
    }
}

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hour>(entity =>
            {
                entity.HasOne(hour => hour.Developer);
            });
            modelBuilder.Entity<Hour>(entity =>
            {
                entity.HasOne(hour => hour.Project);
            });
            modelBuilder.Entity<DeveloperProject>().HasKey(devprj => new { devprj.DeveloperId, devprj.ProjectId });
        }
    }
}

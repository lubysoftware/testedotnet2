using Microsoft.EntityFrameworkCore;
using Api.Models;
using System.Collections.Generic;
using System;

namespace TesteDotnet.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Api.Models.Developer> Developer { get; set; }

        public new DbSet<Api.Models.Entry> Entry { get; set; }

        public DbSet<Api.Models.Project> Project { get; set; }

        public DbSet<Api.Models.DeveloperProject> DeveloperProject { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Developer>().HasKey(dev => new { dev.Id, dev.CPF });
            builder.Entity<DeveloperProject>().HasKey(devPrj => new { devPrj.DeveloperId, devPrj.ProjectId });

            builder.Entity<Developer>().HasData(new List<Developer>() { 
                new Developer(1, "Leonardo", "12345678900", "abc@gmail.com", "1234"),
                new Developer(2, "Maria", "12345678901", "abc1@gmail.com", "1234"),
                new Developer(3, "José", "12345678902", "abc2@gmail.com", "1234"),
            });

            builder.Entity<Project>().HasData(new List<Project>() {
                new Project(1, "Web App"),
                new Project(2, "API"),
                new Project(3, "Database"),
            });

            builder.Entity<Entry>().HasData(new List<Entry>() {
                new Entry(1, DateTime.Now, DateTime.Parse("15/1/2021"), 1, 1),
                new Entry(2, DateTime.Now, DateTime.Parse("15/1/2021"), 2, 2),
            }); ;

            builder.Entity<DeveloperProject>().HasData(new List<DeveloperProject>() {
                new DeveloperProject(1, 1),
                new DeveloperProject(2, 2),
            }); ;
        }
    }
}

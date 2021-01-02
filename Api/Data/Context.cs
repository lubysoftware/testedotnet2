using Microsoft.EntityFrameworkCore;
using Api.Models;
using System.Collections.Generic;
using System;
using TesteDotnet.Models.ViewModels;
using System.Data.Entity.Infrastructure;

namespace TesteDotnet.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Developer> Developer { get; set; }

        public new DbSet<Entry> Entry { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<DeveloperProject> DeveloperProject { get; set; }

        public DbSet<WorkedHoursRank> WorkedHoursRank { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Developer>().HasKey(dev => new { dev.Id, dev.CPF });
            builder.Entity<DeveloperProject>().HasKey(devPrj => new { devPrj.DeveloperId, devPrj.ProjectId });
            builder.Entity<WorkedHoursRank>().HasNoKey();

            builder.Entity<Developer>().HasData(new List<Developer>() {
                new Developer(1, "Leonardo", "54011152021", "leonardo@gmail.com", "1234"),
                new Developer(2, "Maria", "83336894000", "abc1@gmail.com", "1234"),
                new Developer(3, "José", "07426071006", "abc2@gmail.com", "1234"),
                new Developer(4, "Leonardo2", "01928079008", "abc3@gmail.com", "1234"),
                new Developer(5, "Maria2", "13757262000", "abc4@gmail.com", "1234"),
                new Developer(6, "José2", "70610579045", "abc5@gmail.com", "1234"),
                new Developer(7, "Leonardo3", "05827793086", "abc6@gmail.com", "1234"),
                new Developer(8, "Maria3", "65451565007", "abc7@gmail.com", "1234"),
                new Developer(9, "José3", "40687197058", "abc8@gmail.com", "1234"),
                new Developer(10, "Leonardo4", "28596429000", "abc9@gmail.com", "1234"),
                new Developer(11, "Maria4", "54621768050", "abc10@gmail.com", "1234"),
                new Developer(12, "José4", "55705968019", "abc11@gmail.com", "1234"),
            });

            builder.Entity<Project>().HasData(new List<Project>() {
                new Project(1, "Web App"),
                new Project(2, "API"),
                new Project(3, "Database"),
            });

            builder.Entity<Entry>().HasData(new List<Entry>() {
                new Entry(1, DateTime.Parse("15/1/2021 14:00:00"), DateTime.Parse("15/1/2021 15:00:00"), 1, 1),
                new Entry(2, DateTime.Parse("16/1/2021 14:00:00"), DateTime.Parse("16/1/2021 15:00:00"), 1, 1),
                new Entry(3, DateTime.Parse("17/1/2021 14:00:00"), DateTime.Parse("17/1/2021 15:00:00"), 1, 1),
                new Entry(4, DateTime.Parse("18/1/2021 14:00:00"), DateTime.Parse("18/1/2021 15:00:00"), 1, 1),
                new Entry(5, DateTime.Parse("19/1/2021 14:00:00"), DateTime.Parse("19/1/2021 15:00:00"), 1, 1),
                new Entry(7, DateTime.Parse("16/1/2021 14:00:00"), DateTime.Parse("16/1/2021 15:00:00"), 2, 1),
                new Entry(8, DateTime.Parse("17/1/2021 14:00:00"), DateTime.Parse("17/1/2021 15:00:00"), 2, 1),
                new Entry(9, DateTime.Parse("18/1/2021 14:00:00"), DateTime.Parse("18/1/2021 15:00:00"), 2, 1),
                new Entry(10, DateTime.Parse("19/1/2021 14:00:00"), DateTime.Parse("19/1/2021 15:00:00"), 2, 1),
                new Entry(11, DateTime.Parse("18/1/2021 14:00:00"), DateTime.Parse("18/1/2021 15:00:00"), 3, 1),
                new Entry(12, DateTime.Parse("19/1/2021 14:00:00"), DateTime.Parse("19/1/2021 15:00:00"), 3, 1),
                new Entry(13, DateTime.Parse("20/1/2021 14:00:00"), DateTime.Parse("20/1/2021 15:00:00"), 3, 1),
                new Entry(14, DateTime.Parse("19/1/2021 14:00:00"), DateTime.Parse("19/1/2021 15:00:00"), 4, 2),
                new Entry(15, DateTime.Parse("20/1/2021 14:00:00"), DateTime.Parse("20/1/2021 15:00:00"), 4, 2),
                new Entry(16, DateTime.Parse("19/1/2021 14:00:00"), DateTime.Parse("19/1/2021 15:00:00"), 5, 2),
                new Entry(17, DateTime.Parse("20/1/2021 14:00:00"), DateTime.Parse("20/1/2021 15:00:00"), 5, 2),
                new Entry(18, DateTime.Parse("20/1/2021 14:00:00"), DateTime.Parse("20/1/2021 15:00:00"), 6, 3),
            }); ;

            builder.Entity<DeveloperProject>().HasData(new List<DeveloperProject>() {
                new DeveloperProject(1, 1),
                new DeveloperProject(2, 2),
            }); ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Models;

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
    }
}

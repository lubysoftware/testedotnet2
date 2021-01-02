using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.Developers.Entities;
using Tasks.Ifrastructure.Seeders.Developers;

namespace Tasks.Ifrastructure.Seeders
{
    public class TasksSeed
    {
        public void Seed(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<Developer>().HasData(new DeveloperSeeder().GetList());
        }
    }
}

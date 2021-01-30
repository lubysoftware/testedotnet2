using Api_Teste.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Database
{
    public class BaseContext: DbContext
    {
        public BaseContext()
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = TI01\\ELNPLOCAL; Database = Ranking; User Id = sa; Password = 123;");

        }

        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Lancamento> Lancamento { get; set; }
        public DbSet<DevProjeto> DevProjeto { get; set; }
        public DbSet<Desenvolvedor> Desenvolvedor { get; set; }
    }
}

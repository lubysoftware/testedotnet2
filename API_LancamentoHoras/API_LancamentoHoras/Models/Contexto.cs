using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_LancamentoHoras.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<LancamentoHoras> LancamentoHorass { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
    }
}

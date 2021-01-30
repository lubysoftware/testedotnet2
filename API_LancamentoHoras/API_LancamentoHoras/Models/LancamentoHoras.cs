using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class LancamentoHoras
    {
        public int Id { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int DesenvolvedorId { get; set; }
        public int ProjetoId { get; set; }

        public Desenvolvedor Desenvolvedor { get; set; }
        public Projeto Projeto { get; set; }
    }
}

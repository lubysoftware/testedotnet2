using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class ProjetoDesenvolvedor
    {
        public int ProjetoId { get; set; }
        public int DesenvolvedorId { get; set; }

        public Projeto Projeto { get; set; }
        public Desenvolvedor Desenvolvedor { get; set; }
    }
}

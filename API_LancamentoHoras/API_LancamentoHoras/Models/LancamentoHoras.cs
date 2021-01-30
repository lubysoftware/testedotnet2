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

        public virtual Desenvolvedor Desenvolvedor { get; set; }
        public virtual Projeto Projeto { get; set; }

        public LancamentoHoras() { }

        public LancamentoHoras(int Id, DateTime DataInicial, DateTime DataFinal, int DesenvolvedorId, int ProjetoId)
        {
            this.Id = Id;
            this.DataInicial = DataInicial;
            this.DataFinal = DataFinal;
            this.DesenvolvedorId = DesenvolvedorId;
            this.ProjetoId = ProjetoId;
        }
    }
}

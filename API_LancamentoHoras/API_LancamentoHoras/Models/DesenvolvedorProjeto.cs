using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class DesenvolvedorProjeto
    {
        public int DesenvolvedorId { get; set; }
        public int ProjetoId { get; set; }

        public virtual Desenvolvedor Desenvolvedor { get; set; }
        public virtual Projeto Projeto { get; set; }

        public DesenvolvedorProjeto() { }

        public DesenvolvedorProjeto(int DesenvolvedorId, int ProjetoId)
        {
            this.DesenvolvedorId = DesenvolvedorId;
            this.ProjetoId = ProjetoId;
        }
    }
}

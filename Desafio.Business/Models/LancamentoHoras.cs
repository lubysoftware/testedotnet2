using System;

namespace Desafio.Business.Models
{
    public class LancamentoHoras : Entity
    {
        public int DesenvolvedorId { get; set; }
        public int ProjetoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }


        /* EF Relation */
        public virtual Desenvolvedor Desenvolvedor { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}

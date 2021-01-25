using System.Collections.Generic;


namespace Desafio.Business.Models
{
    public class Projeto : Entity
    {
        public string Nome { get; set; }



        /* EF Relation */
        public virtual IEnumerable<LancamentoHoras> Lancamentos { get; set; }
        public virtual IEnumerable<ProjetoDesenvolvedor> ProjetosDesenvolvedores { get; set; }

    }
}

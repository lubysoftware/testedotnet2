using System.Collections.Generic;

namespace Desafio.Business.Models
{
    public class Desenvolvedor : Entity
    {  
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        /* EF Relation */
        public virtual IEnumerable<LancamentoHoras> Lancamentos { get; set; }
        public virtual IEnumerable<ProjetoDesenvolvedor> ProjetosDesenvolvedores { get; set; }

    }
}
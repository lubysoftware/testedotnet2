using System.Collections.Generic;


namespace Desafio.Business.Models
{
    public class ProjetoDesenvolvedor : Entity
    {
        public int ProjetoID { get; set; }
        public int DesenvolvedorID { get; set; }



        /* EF Relation */
        public virtual Projeto Projeto { get; set; }
        public virtual Desenvolvedor Desenvolvedor { get; set; }

    }
}

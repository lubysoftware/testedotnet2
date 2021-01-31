using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class Desenvolvedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public virtual IEnumerable<LancamentoHoras> LancamentosHoras { get; set; }
        public virtual IEnumerable<DesenvolvedorProjeto> DesenvolvedoresProjetos { get; set; }

        public Desenvolvedor() { }

        public Desenvolvedor(int Id, string Nome, string Cpf)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Cpf = Cpf;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<LancamentoHoras> LancamentosHoras { get; set; }
        public virtual ICollection<DesenvolvedorProjeto> ProjetosDesenvolvedores { get; set; }

        public Projeto() { }

        public Projeto(int Id, string Descricao)
        {
            this.Id = Id;
            this.Descricao = Descricao;
        }
    }
}

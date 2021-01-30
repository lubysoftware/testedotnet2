using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Models
{
    public class DevProjeto
    {
        [Key]
        public int DevProjetoid { get; set; }

        [ForeignKey("Desenvolvedor")]
        public int DesenvolvedorId { get; set; }
        public virtual Desenvolvedor Desenvolvedor { get; set; }

        [ForeignKey ("Projeto")]
        public int ProjetoId { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}

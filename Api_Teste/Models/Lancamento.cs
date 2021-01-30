using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Models
{
    public class Lancamento
    {
        [Key]
        public int LancamentoId { get; set; }

        [ForeignKey("Desenvolvedor")]
        public int DesenvolvedorId { get; set; }
        public virtual Desenvolvedor Desenvolvedor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Models
{
    public class Desenvolvedor
    {
        [Key]
        public int DesenvolvedorId { get; set; }

        public string Nome { get; set; }

        public int CPF { get; set; }
    }
}

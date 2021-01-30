using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Models
{
    public class Projeto
    {   
        [Key]
        public int ProjetoId { get; set; }
       
        public string Descricao { get; set; }
    }
}

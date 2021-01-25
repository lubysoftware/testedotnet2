using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Business.DTO
{
    public class RanqueamentoDTO
    {
        public int DesenvolvedorID { get; set; }
        public string Nome { get; set; }
        public decimal TotalHoras { get; set; }
        public int DiasTrabalhados { get; set; }
        public decimal MediaHoras { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class Validacao
    {
        public string Message { get; set; }

        public Validacao(string Message)
        {
            this.Message = Message;
        }
    }
}

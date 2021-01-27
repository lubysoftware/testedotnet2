using System;

namespace DesafioLuby.Models
{
    public class HorasLancamentoModel
    {

        public int IdHorasLancamento { get; set; }
        public int IdDesenvolvedor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}

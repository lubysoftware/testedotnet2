using System;
using System.Collections.Generic;

#nullable disable

namespace Luby.Infra.Context
{
    public partial class Lancamento
    {
        public int Id { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int IdDesenvolvedor { get; set; }
        public int? IdProjeto { get; set; }
    }
}

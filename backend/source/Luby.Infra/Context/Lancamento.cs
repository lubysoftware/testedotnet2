using System;
using System.Collections.Generic;
using Luby.Infra.Context;

#nullable disable

namespace Luby.Infra.Context
{
    public partial class Lancamento:BaseEntity
    {
        //public int Id { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int IdDesenvolvedor { get; set; }
        public int? IdProjeto { get; set; }
    }
}

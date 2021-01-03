using System;
using System.Collections.Generic;
using Luby.Infra.Context;

#nullable disable

namespace Luby.Infra.Context
{
    public partial class Equipeprojeto
    {
        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public int IdDesenvolvedor { get; set; }
    }
}

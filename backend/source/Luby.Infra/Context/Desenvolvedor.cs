using System;
using System.Collections.Generic;
using Luby.Infra.Context;

#nullable disable

namespace Luby.Infra.Context
{
    public partial class Desenvolvedor:BaseEntity
    {
       // public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}

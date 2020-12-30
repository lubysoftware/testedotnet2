using System;
using System.Collections.Generic;

#nullable disable

namespace Luby.Infra.Context
{
    public partial class Desenvolvedor
    {
        public int Id { get; set; }
        public int Nome { get; set; }
        public string Cpf { get; set; }
        public int Cargo { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}

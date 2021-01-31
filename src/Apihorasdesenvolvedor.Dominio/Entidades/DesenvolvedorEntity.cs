using System;

namespace Apihorasdesenvolvedor.Dominio.Entidades
{
    public class DesenvolvedorEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

    }
}

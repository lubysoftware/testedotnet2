using luby_app.Domain.Common;
using System.Collections.Generic;

namespace luby_app.Domain.Entities
{
    public class Desenvolvedor : AuditableEntity
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public int ProjetoId { get; set; }

        public Projeto Projeto { get; set; }

        public string UsuarioId { get; set; }

        public IList<DesenvolvedorHora> DesenvolvedorHora { get; set; } = new List<DesenvolvedorHora>();
    }
}

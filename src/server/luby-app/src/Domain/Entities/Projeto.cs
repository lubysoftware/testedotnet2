using luby_app.Domain.Common;
using System.Collections.Generic;

namespace luby_app.Domain.Entities
{
    public class Projeto : AuditableEntity
    {
        public int Id { get; set; }

        public string Nome { get; set; } 

        public IList<Desenvolvedor> Desenvolvedores { get; private set; } = new List<Desenvolvedor>();
    }
}

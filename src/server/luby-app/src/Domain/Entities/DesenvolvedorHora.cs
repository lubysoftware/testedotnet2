using luby_app.Domain.Common;
using System;
using System.Collections.Generic;

namespace luby_app.Domain.Entities
{
    public class DesenvolvedorHora : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public TimeSpan TotalHoras() => (this.Fim - this.Inicio);

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }

        public int DesenvolvedorId { get; set; }

        public virtual Desenvolvedor Desenvolvedor { get; set; }
         
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}

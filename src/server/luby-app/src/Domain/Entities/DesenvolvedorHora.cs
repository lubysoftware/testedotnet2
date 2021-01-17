using luby_app.Domain.Common;
using System;

namespace luby_app.Domain.Entities
{
    public class DesenvolvedorHora : AuditableEntity
    {
        public int Id { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public double TotalHoras { get { return (this.Fim - this.Inicio).TotalHours; } }

        public int DesenvolvedorId { get; set; }

        public Desenvolvedor Desenvolvedor { get; set; }
    }
}

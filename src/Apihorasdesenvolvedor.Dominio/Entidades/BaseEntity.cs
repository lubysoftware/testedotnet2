using System;
using System.ComponentModel.DataAnnotations;

namespace Apihorasdesenvolvedor.Dominio.Entidades
{
    public abstract class BaseEntity
    {
        [Key]
        public int id { get; set; }

        private DateTime _criadoEm;
        public DateTime CriadoEm
        {
            get { return _criadoEm; }
            set { _criadoEm = (value == null ? DateTime.UtcNow : value); }
        }
        public DateTime? AlteradoEm { get; set; }

    }
}

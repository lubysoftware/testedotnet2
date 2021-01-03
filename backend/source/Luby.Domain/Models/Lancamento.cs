using System;
using System.Collections.Generic;

namespace Luby.Domain.Models
{
    public class Lancamento : BaseEntity
    {
        public Lancamento(int id,DateTime DtInicio, DateTime DtFim, int IdDesenvolvedor, int IdProjeto)
        {
            this.IdDesenvolvedor=id;
            this.DtInicio = DtInicio;
            this.DtFim = DtFim;
            this.IdDesenvolvedor = IdDesenvolvedor;
            this.IdProjeto = IdProjeto;
        }

        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int IdDesenvolvedor { get; set; }
        public int IdProjeto { get; set; }


        public void Update(DateTime DtInicio, DateTime DtFim, int IdDesenvolvedor,
        int IdProjeto)
        {

        }
        private void ValidaLancamento(DateTime DtInicio, DateTime DtFim, int IdDesenvolvedor,
        int IdProjeto)
        {

        }
    }
}
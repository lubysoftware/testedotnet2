using System;

namespace Apihorasdesenvolvedor.Dominio.Entidades
{
    public class DesenvolvedorXLancamentohorasEntity : BaseEntity
    {
        public int Fk_Desenvolvedor { get; set; }
        public int Fk_Projeto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}

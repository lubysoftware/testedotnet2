using System;

namespace Apihorasdesenvolvedor.Dominio.Entidades
{
    public class DesenvolvedorXProjetoEntity : BaseEntity
    {
        public int Fk_Desenvolvedor { get; set; }
        public int Fk_Projeto { get; set; }

    }
}

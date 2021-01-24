using ControleHoras.API.BaseModels;
using System;

namespace ControleHoras.API.EntityModels
{
    public class DesenvolvedorProjeto : EntityModelBase
    {
        public int IdDesenvolvedor { get; set; }
        public Desenvolvedor Desenvolvedor { get; set; }

        public int IdProjeto { get; set; }
        public Projeto Projeto { get; set; }

    }
}

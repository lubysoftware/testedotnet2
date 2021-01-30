using Api_Teste.Database;
using Api_Teste.Models;
using Api_Teste.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Service
{
    public class ProjetoService : IProjetoRepository
    {
        private BaseContext baseContext;

        public ProjetoService()
        {
            baseContext = new BaseContext();
        }
        public void CadastrarProjeto(Projeto projeto)
        {
            if (!String.IsNullOrEmpty(projeto.Descricao))
            {
                baseContext.Projeto.Add(projeto);
                baseContext.SaveChanges();
            }
        }

        public void ExcluirProjeto(int id)
        {
            if (id != 0)
            {
                var projeto = baseContext.Projeto.Find(id);
                if (projeto != null)
                {
                    baseContext.Remove(projeto);
                    baseContext.SaveChanges();
                }

            }
        }
        
        public List<Projeto> ListarProjeto()
        {
            return baseContext.Projeto.ToList();
        }

        public Projeto ObterProjeto(int id)
        {
            var projeto = baseContext.Projeto.Find(id);

            if (projeto != null)
            {
                return projeto;
            }
            return null;
        }


        public void AtualizarProjeto(int id, Projeto projeto)
        {
            if (!String.IsNullOrEmpty(projeto.Descricao) && (id != 0))
            {
                var projetoAux = baseContext.Projeto.Find(id);
                if (projetoAux != null)
                {
                    baseContext.Entry(projetoAux).State = EntityState.Modified;
                    projetoAux.Descricao = projeto.Descricao;
                    baseContext.SaveChanges();
                }

            }
        }
    }
}

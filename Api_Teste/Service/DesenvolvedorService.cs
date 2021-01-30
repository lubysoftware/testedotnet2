using Api_Teste.Database;
using Api_Teste.Models;
using Api_Teste.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Teste.Service
{
    public class DesenvolvedorService: IDesenvolvedorRepository
    {
        private BaseContext baseContext;
       
        public DesenvolvedorService()
        {
          baseContext = new BaseContext();
        }

        public void AtualizarDesenvolvedor(int id,Desenvolvedor desenvolvedor)
        {
            if (!String.IsNullOrEmpty(desenvolvedor.Nome)&& (id != 0))
            {
                var desenvolvedorAux = baseContext.Desenvolvedor.Find(id);
                if (desenvolvedorAux != null)
                {
                  
                    baseContext.Entry(desenvolvedorAux).State = EntityState.Modified;
                    desenvolvedorAux.Nome = desenvolvedor.Nome;
                    baseContext.SaveChanges();
                }

            }
        }

        public void CadastrarDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            if (!String.IsNullOrEmpty(desenvolvedor.Nome))
            {
                baseContext.Desenvolvedor.Add(desenvolvedor);
                baseContext.SaveChanges();
            }
          
        }

        public void ExcluirDesenvolvedor(int id)
        {
            if (id != 0)
            {
                var desenvolvedor = baseContext.Desenvolvedor.Find(id);
                if (desenvolvedor != null)
                {
                    baseContext.Remove(desenvolvedor);
                    baseContext.SaveChanges();
                }
                
            }
        }

        public List<Desenvolvedor> ListarDesenvolvedor()
        {
            var baseContext = new BaseContext();

            return baseContext.Desenvolvedor.ToList();
           
        }

        public Desenvolvedor ObterDesenvolvedor(int id)
        {
            var desenvolvedor = baseContext.Desenvolvedor.Find(id);

            if (desenvolvedor != null)
            {
                return desenvolvedor;
            }
            return null;
        }
    }
}

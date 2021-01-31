using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dados.Contexto;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXLancamentohoras;
using Microsoft.EntityFrameworkCore;

namespace Apihorasdesenvolvedor.Servico.Servicos.BO
{
    public class RelatorioHoras
    {

        public RelatorioHoras(IRepositorio<DesenvolvedorXLancamentohorasEntity> repositoriolancamentohoras)
        {
            _repositoriolancamentohoras = repositoriolancamentohoras;

        }

        private IRepositorio<DesenvolvedorXLancamentohorasEntity> _repositoriolancamentohoras;
        public Task<IEnumerable<DesenvolvedorXLancamentohorasEntity>> TopFiveDesenvolvedoresAsync() //Checar os 5 Devs com mais horas trabalhadas
        {

            //Incorporar aqui o retorno dos 5.

            //tera que trabalhar essa logica de criação desta injeção do metodo, pois deve retornar 5 PK-Desenvolvedores




            return null;
        }
    }
}

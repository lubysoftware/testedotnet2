using Dapper;
using DesafioLuby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Canducci.Pagination;
namespace DesafioLuby.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ServiceBase
    {
        ServiceDesenvolvedor service = new ServiceDesenvolvedor();

        [HttpGet("page/{page?}")]
        public async Task<PaginatedRest<DesenvolvedorModel>> GetAsync(int? IdDesenvolvedor, int? page)
        {
            page ??= 1;
            if (page <= 0) page = 1;

            return await service.SelectAsync(IdDesenvolvedor, page);
        }


        [HttpPut]
        public async Task<string> PutAsync(DesenvolvedorModel desenvolvedor)
        {

            var sucesso = await service.InsertAsync(desenvolvedor);

            return "Registro salvo com sucesso!";
        }


        [HttpPost]
        public async Task<string> PostAsync(DesenvolvedorModel desenvolvedor)
        {
            if (desenvolvedor.IdDesenvolvedor > 0)
            {

                var result = await service.UpdateAsync(desenvolvedor);
                return "Registro alterado com sucesso!";

            }
            else
                return "O Id tem que ser maior que 0";

        }

        [HttpDelete]
        public async Task<string> DeleteAsync(int IdDesenvolvedor)
        {
            if (IdDesenvolvedor > 0)
            {
                var resultado = await service.DeleteAsync(IdDesenvolvedor);
                return resultado.ToString();
            }
            else
                return "O Id tem que ser maior que 0";



        }
    }

}


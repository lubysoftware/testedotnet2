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
        ServiceProjeto service = new ServiceProjeto();

        [HttpGet("projetoAsync/{page?}")]
        public async Task<PaginatedRest<ProjetoModel>> GetAsync(int? IdProjeto, int? page)
        {
            page ??= 1;
            if (page <= 0) page = 1;
             return await service.SelectAsync(0, page);
        }

        [HttpGet("projeto/")]
        public async Task<List<ProjetoModel>> Get(int? IdProjeto)
        {
             return service.SelectProj(0);
        }


        [HttpPut]
        public async Task<string> PutAsync(ProjetoModel horas)
        {

            var sucesso = await service.InsertAsync(horas);

            return "Registro salvo com sucesso!";
        }


        [HttpPost]
        public async Task<string> PostAsync(ProjetoModel desenvolvedor)
        {
            if (desenvolvedor.IdProjeto > 0)
            {

                var result = await service.UpdateAsync(desenvolvedor);
                return "Registro alterado com sucesso!";

            }
            else
                return "O Id tem que ser maior que 0";

        }

       
    }

}


using luby_app.Application.Desenvolvedor.Queries.GetRankingDesenvolvedor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luby_app.WebUI.Controllers
{
    public class DesenvolvedorRankingController : ApiControllerBase
    { 
        [HttpGet]
        public async Task<ActionResult<List<RankingDto>>> GetRankingDesenvolvedor([FromQuery] GetRankingDesenvolvedorQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}

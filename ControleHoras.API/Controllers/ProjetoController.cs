using ControleHoras.API.BaseModels;
using ControleHoras.API.EntityModels;
using ControleHoras.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleHoras.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProjetosController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get(int skip, int take)
        {
            return Ok(ProjetoRepository.Instance.PagedGet(skip, take));
        }

        [HttpGet]
        [Route("ById")]
        public IActionResult ById(int id)
        {
            return Ok(ProjetoRepository.Instance.ById(id));
        }

        [HttpPost]
        public IActionResult Post(ProjetoValidable projeto)
        {
            return Ok(ProjetoRepository.Instance.Insert(new Projeto(projeto)));
        }
    }
}
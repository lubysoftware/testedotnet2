using ControleHoras.API.BaseModels;
using ControleHoras.API.EntityModels;
using ControleHoras.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleHoras.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize()]
    public class DesenvolvedorController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get(int skip, int take)
        {
            return Ok(DesenvolvedorRepository.Instance.PagedGet(skip, take));
        }

        [HttpGet]
        [Route("ById")]
        public IActionResult ById(int id)
        {
            return Ok(DesenvolvedorRepository.Instance.ById(id));
        }

        [HttpPost]
        public IActionResult Post(DesenvolvedorValidable desenvolvedor)
        {
            return Ok(DesenvolvedorRepository.Instance.Insert(new Desenvolvedor(desenvolvedor)));
        }
    }
}
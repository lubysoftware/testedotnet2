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
    public class LancamentoController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get(int skip, int take)
        {
            return Ok(LancamentoRepository.Instance.PagedGet(skip, take));
        }

        [HttpGet]
        [Route("ById")]
        public IActionResult ById(int id)
        {
            return Ok(LancamentoRepository.Instance.ById(id));
        }

        [HttpPost]
        public IActionResult Post(LancamentoValidable lancamento)
        {
            if (UserHaveNoRelationsWithInsertedIdProjeto(IdDesenvolvedor, lancamento.IdProjeto.Value))
            {
                ModelState.AddModelError(nameof(lancamento.IdProjeto), "You are not in this project");
                return ValidationProblem(ModelState);
            }
            return Ok(LancamentoRepository.Instance.Insert(new Lancamento(lancamento, IdDesenvolvedor)));
        }

        [HttpGet]
        [Route("TopFiveOfWeekByWorkedHours")]
        public IActionResult TopFiveOfWeekByWorkedHours()
        {
            var result = LancamentoRepository.Instance.TopFiveOfWeekByWorkedHours();

            return Ok(result);
        }

        private bool UserHaveNoRelationsWithInsertedIdProjeto(int idDesenvolvedor, int idProjeto)
           => DesenvolvedorProjetoRepository.Instance.FilterByIdDesenvolvedorIdProjeto(idDesenvolvedor, idProjeto).Count == 0;
    }
}
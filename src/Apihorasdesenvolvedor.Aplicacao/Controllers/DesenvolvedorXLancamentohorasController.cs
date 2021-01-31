using System;
using System.Net;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXLancamentohoras;
using Microsoft.AspNetCore.Mvc;

namespace Apihorasdesenvolvedor.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesenvolvedorXLancamentohorasController : ControllerBase
    {
        private IDesenvolvedorXLancamentohorasService _servicodesenvolvedorxlancamentohoras;
        public DesenvolvedorXLancamentohorasController(IDesenvolvedorXLancamentohorasService service)
        {
            _servicodesenvolvedorxlancamentohoras = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IDesenvolvedorXLancamentohorasService service)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //Solicitacao Indevida 400

            try
            {
                return Ok(await service.GetAll());
            }
            catch (ArgumentException ex) //200 Sucesso
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetDesenvolvedorXLancamentohorasWithId")]
        public async Task<ActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _servicodesenvolvedorxlancamentohoras.GetFiveTop());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpGet("GetFiveTop")]
        public async Task<ActionResult> GetFiveTop([FromServices] IDesenvolvedorXLancamentohorasService service)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //Solicitacao Indevida 400

            try
            {
                return Ok(await service.GetFiveTop());
            }
            catch (ArgumentException ex) //200 Sucesso
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DesenvolvedorXLancamentohorasEntity desenvolvedorxlancamentohoras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _servicodesenvolvedorxlancamentohoras.Post(desenvolvedorxlancamentohoras);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetDesenvolvedorXLancamentohorasWithId", new { id = result.id })), result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] DesenvolvedorXLancamentohorasEntity desenvolvedorxlancamentohoras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _servicodesenvolvedorxlancamentohoras.Put(desenvolvedorxlancamentohoras);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _servicodesenvolvedorxlancamentohoras.Delete(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}

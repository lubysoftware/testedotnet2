using System;
using System.Net;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Desenvolvedor;
using Microsoft.AspNetCore.Mvc;

namespace Apihorasdesenvolvedor.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DesenvolvedoresController : ControllerBase
    {
        private IDesenvolvedorService _servicodesenvolvedor;
        public DesenvolvedoresController(IDesenvolvedorService service)
        {
            _servicodesenvolvedor = service;
        }


      
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IDesenvolvedorService service)
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
        [Route("{id}", Name = "GetDesenvolvedorWithId")]
        public async Task<ActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _servicodesenvolvedor.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DesenvolvedorEntity desenvolvedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _servicodesenvolvedor.Post(desenvolvedor);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetDesenvolvedorWithId", new { id = result.id })), result);
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
        public async Task<ActionResult> Put([FromBody] DesenvolvedorEntity desenvolvedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _servicodesenvolvedor.Put(desenvolvedor);
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
                return Ok(await _servicodesenvolvedor.Delete(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}

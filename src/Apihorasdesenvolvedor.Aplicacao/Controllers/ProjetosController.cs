using System;
using System.Net;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Projeto;
using Microsoft.AspNetCore.Mvc;

namespace Apihorasdesenvolvedor.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {

        private IProjetoService _servicoprojetos;
        public ProjetosController(IProjetoService service)
        {
            _servicoprojetos = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IProjetoService service)
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
        [Route("{id}", Name = "GetProjetoWithId")]
        public async Task<ActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _servicoprojetos.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProjetoEntity projeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _servicoprojetos.Post(projeto);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetProjetoWithId", new { id = result.id })), result);
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

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProjetoEntity projeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _servicoprojetos.Put(projeto);
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
                return Ok(await _servicoprojetos.Delete(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }

}

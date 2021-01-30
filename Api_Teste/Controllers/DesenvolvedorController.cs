using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Teste.Database;
using Api_Teste.Models;
using Api_Teste.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api_Teste.Controllers
{

    public class DesenvolvedorController : Controller
    {
        private DesenvolvedorService desenvolvedorService;

        [HttpGet("v1/desenvolvedor")]
        public IActionResult ListarDesenvolvedor()
        {
            List<Desenvolvedor> desenvolvedor;
            desenvolvedorService = new DesenvolvedorService();
            desenvolvedor = desenvolvedorService.ListarDesenvolvedor();
            return Ok(desenvolvedor);
        }

        [HttpGet("v1/desenvolvedor/{id}")]
        public IActionResult ObterDesenvolvedor(int id)
        {
            desenvolvedorService = new DesenvolvedorService();
            var desenvolvedor = desenvolvedorService.ObterDesenvolvedor(id);
            return Ok(desenvolvedor);

        }

        [HttpPost("v1/desenvolvedor")]
        public IActionResult CadastrarDesenvolvedor([FromBody] Desenvolvedor desenvolvedor)
        {

            desenvolvedorService = new DesenvolvedorService();
            desenvolvedorService.CadastrarDesenvolvedor(desenvolvedor);
            return Ok(desenvolvedor);

        }

        [HttpPut("v1/desenvolvedor/{id}")]
        public IActionResult AlterarDevesonolvedor(int id,[FromBody] Desenvolvedor desenvolvedor)
        {
            desenvolvedorService = new DesenvolvedorService();
            desenvolvedorService.AtualizarDesenvolvedor(id,desenvolvedor);
            return Ok();
        }

        [HttpDelete("v1/desenvolvedor/{id}")]
        public IActionResult ExcluirDesenvolvedor(int id)
        {
            desenvolvedorService = new DesenvolvedorService();
            desenvolvedorService.ExcluirDesenvolvedor(id);
            return Ok();

        }

    }
}

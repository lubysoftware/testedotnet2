using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Teste.Models;
using Api_Teste.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api_Teste.Controllers
{
    public class ProjetoController : Controller
    {
        private ProjetoService projetoService;

        [HttpGet("v1/projeto")]
        public IActionResult ListarProjeto()
        {
            List<Projeto> Projeto;
            projetoService = new ProjetoService();
            Projeto = projetoService.ListarProjeto();
            return Ok(Projeto);

        }

        [HttpGet("v1/projeto/{id}")]
        public IActionResult ObterProjeto(int id)
        {
            projetoService = new ProjetoService();
            var projeto = projetoService.ObterProjeto(id);
            return Ok(projeto);

        }

        [HttpPost("v1/projeto")]
        public IActionResult CadastrarProjeto([FromBody] Projeto projeto)
        {
            projetoService = new ProjetoService();
            projetoService.CadastrarProjeto(projeto);
            return Ok(projeto);
        }

        [HttpPut("v1/projeto/{id}")]
        public IActionResult AlterarProjeto(int id, [FromBody] Projeto projeto)
        {
            projetoService = new ProjetoService();
            projetoService.AtualizarProjeto(id, projeto);
            return Ok();
        }

        [HttpDelete("v1/projeto/{id}")]
        public IActionResult ExcluirProjeto(int id)
        {
            projetoService = new ProjetoService();
            projetoService.ExcluirProjeto(id);
            return Ok();

        }
    }
}

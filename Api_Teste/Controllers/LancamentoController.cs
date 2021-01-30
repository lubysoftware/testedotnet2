using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Teste.Models;
using Api_Teste.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api_Teste.Controllers
{
    public class LancamentoController : Controller   
    {
        private LancamentoService lancamentoService;

        [HttpPost("v1/lancamento")]
        public IActionResult CadastrarLancamento( Lancamento lancamento)
        {
            lancamentoService = new LancamentoService();
            lancamentoService.CadastrarLancamento(lancamento);
            return Ok(lancamento);
        }
    }
}

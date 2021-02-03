using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LancamentoHoras.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_LancamentoHoras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesenvolvedorProjetoController : ControllerBase
    {
        private readonly Contexto _context;

        public DesenvolvedorProjetoController(Contexto context)
        {
            _context = context;
        }

        // GET: api/DesenvolvedorProjeto
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DesenvolvedorProjeto>>> GetProjetoDesenvolvedor()
        {
            try
            {
                return await _context.DesenvolvedorProjeto.ToListAsync();
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro listar projetos" });
            }
        }

        // POST: api/DesenvolvedorProjeto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DesenvolvedorProjeto>> PostDesenvolvedorProjeto(DesenvolvedorProjeto desenvolvedorProjeto)
        {
            try
            {
                var query = _context.DesenvolvedorProjeto.Where(x =>
                x.DesenvolvedorId == desenvolvedorProjeto.DesenvolvedorId &&
                x.ProjetoId == desenvolvedorProjeto.ProjetoId);

                if (query.Count() > 0)
                    return Ok(new Validacao("Este projeto já estava cadastrado ao desenvolvedor"));

                _context.DesenvolvedorProjeto.Add(desenvolvedorProjeto);

                await _context.SaveChangesAsync();

                return Ok(new Validacao("Sucesso ao adicionar projeto para o desenvolvedor"));
            }
            catch (DbUpdateException)
            {
                return NotFound(new { Erro = "Erro ao incluir projeto ao desenvolvedor" });
            }
        }

        // DELETE: api/DesenvolvedorProjeto/5
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteDesenvolvedorProjeto([FromBody] DesenvolvedorProjeto dp)
        {
            try
            {
                var desenvolvedorProjeto = _context.DesenvolvedorProjeto
                    .Where(x => x.DesenvolvedorId == dp.DesenvolvedorId && x.ProjetoId == dp.ProjetoId);

                if (desenvolvedorProjeto.Count() == 0)
                    return Ok(new Validacao("Relação desenvolvedor-projeto inexistente"));

                _context.DesenvolvedorProjeto.Remove(desenvolvedorProjeto.FirstOrDefault());
                await _context.SaveChangesAsync();

                return Ok(new Validacao("Excluído com successo"));
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao deletar relação desenvolvedor projeto" });
            }
        }

        private bool DesenvolvedorProjetoExists(int id)
        {
            return _context.DesenvolvedorProjeto.Any(e => e.ProjetoId == id);
        }
    }
}

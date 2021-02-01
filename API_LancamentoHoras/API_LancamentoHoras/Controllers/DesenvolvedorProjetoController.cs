using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LancamentoHoras.Models;

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
        public async Task<ActionResult<IEnumerable<DesenvolvedorProjeto>>> GetProjetoDesenvolvedor()
        {
            return await _context.DesenvolvedorProjeto.ToListAsync();
        }

        // GET: api/DesenvolvedorProjeto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DesenvolvedorProjeto>> GetDesenvolvedorProjeto(int id)
        {
            var desenvolvedorProjeto = await _context.DesenvolvedorProjeto.FindAsync(id);

            if (desenvolvedorProjeto == null)
            {
                return NotFound();
            }

            return desenvolvedorProjeto;
        }

        // POST: api/DesenvolvedorProjeto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DesenvolvedorProjeto>> PostDesenvolvedorProjeto(DesenvolvedorProjeto desenvolvedorProjeto)
        {
            _context.DesenvolvedorProjeto.Add(desenvolvedorProjeto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DesenvolvedorProjetoExists(desenvolvedorProjeto.ProjetoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDesenvolvedorProjeto", new { id = desenvolvedorProjeto.ProjetoId }, desenvolvedorProjeto);
        }

        // DELETE: api/DesenvolvedorProjeto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesenvolvedorProjeto(int id)
        {
            var desenvolvedorProjeto = await _context.DesenvolvedorProjeto.FindAsync(id);
            if (desenvolvedorProjeto == null)
            {
                return NotFound();
            }

            _context.DesenvolvedorProjeto.Remove(desenvolvedorProjeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesenvolvedorProjetoExists(int id)
        {
            return _context.DesenvolvedorProjeto.Any(e => e.ProjetoId == id);
        }
    }
}

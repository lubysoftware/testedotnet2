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
    public class LancamentoHorasController : ControllerBase
    {
        private readonly Contexto _context;

        public LancamentoHorasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/LancamentoHoras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LancamentoHoras>>> GetLancamentoHoras()
        {
            return await _context.LancamentoHoras.ToListAsync();
        }

        // GET: api/LancamentoHoras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LancamentoHoras>> GetLancamentoHoras(int id)
        {
            IQueryable<LancamentoHoras> query = _context.LancamentoHoras;
            query = query.Include(p => p.Desenvolvedor);
            query = query.Include(p => p.Projeto);
            query = query.Where(a => a.Id == id);

            var lancamentoHoras = await query.FirstOrDefaultAsync();

            if (lancamentoHoras == null)
            {
                return NotFound();
            }

            return lancamentoHoras;
        }

        // POST: api/LancamentoHoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Validacao>> PostLancamentoHoras(LancamentoHoras lancamentoHoras)
        {
            try
            {
                var desenvolvedorProjeto = _context.DesenvolvedorProjeto
                .FirstOrDefault(dp =>
                dp.DesenvolvedorId == lancamentoHoras.DesenvolvedorId &&
                dp.ProjetoId == lancamentoHoras.ProjetoId);

                if (desenvolvedorProjeto == null)
                {
                    return Ok("O projeto não está vinculado ao desenvolvedor");
                }

                _context.LancamentoHoras.Add(lancamentoHoras);
                await _context.SaveChangesAsync();

                return Ok(new Validacao("Enviado"));
            }
            catch (Exception)
            {
                return Ok("Erro no lançamento das Horas");
            }
        }

        // DELETE: api/LancamentoHoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLancamentoHoras(int id)
        {
            var lancamentoHoras = await _context.LancamentoHoras.FindAsync(id);
            if (lancamentoHoras == null)
            {
                return NotFound();
            }

            _context.LancamentoHoras.Remove(lancamentoHoras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LancamentoHorasExists(int id)
        {
            return _context.LancamentoHoras.Any(e => e.Id == id);
        }
    }
}

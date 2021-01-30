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
    public class ProjetoDesenvolvedorsController : ControllerBase
    {
        private readonly Contexto _context;

        public ProjetoDesenvolvedorsController(Contexto context)
        {
            _context = context;
        }

        // GET: api/ProjetoDesenvolvedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoDesenvolvedor>>> GetProjetoDesenvolvedor()
        {
            return await _context.ProjetoDesenvolvedor.ToListAsync();
        }

        // GET: api/ProjetoDesenvolvedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoDesenvolvedor>> GetProjetoDesenvolvedor(int id)
        {
            var projetoDesenvolvedor = await _context.ProjetoDesenvolvedor.FindAsync(id);

            if (projetoDesenvolvedor == null)
            {
                return NotFound();
            }

            return projetoDesenvolvedor;
        }

        // PUT: api/ProjetoDesenvolvedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjetoDesenvolvedor(int id, ProjetoDesenvolvedor projetoDesenvolvedor)
        {
            if (id != projetoDesenvolvedor.ProjetoId)
            {
                return BadRequest();
            }

            _context.Entry(projetoDesenvolvedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoDesenvolvedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjetoDesenvolvedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjetoDesenvolvedor>> PostProjetoDesenvolvedor(ProjetoDesenvolvedor projetoDesenvolvedor)
        {
            _context.ProjetoDesenvolvedor.Add(projetoDesenvolvedor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjetoDesenvolvedorExists(projetoDesenvolvedor.ProjetoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjetoDesenvolvedor", new { id = projetoDesenvolvedor.ProjetoId }, projetoDesenvolvedor);
        }

        // DELETE: api/ProjetoDesenvolvedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjetoDesenvolvedor(int id)
        {
            var projetoDesenvolvedor = await _context.ProjetoDesenvolvedor.FindAsync(id);
            if (projetoDesenvolvedor == null)
            {
                return NotFound();
            }

            _context.ProjetoDesenvolvedor.Remove(projetoDesenvolvedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjetoDesenvolvedorExists(int id)
        {
            return _context.ProjetoDesenvolvedor.Any(e => e.ProjetoId == id);
        }
    }
}

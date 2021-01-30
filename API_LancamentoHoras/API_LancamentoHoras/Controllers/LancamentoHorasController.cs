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
            var lancamentoHoras = await _context.LancamentoHoras.FindAsync(id);

            if (lancamentoHoras == null)
            {
                return NotFound();
            }

            return lancamentoHoras;
        }

        // PUT: api/LancamentoHoras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLancamentoHoras(int id, LancamentoHoras lancamentoHoras)
        {
            if (id != lancamentoHoras.Id)
            {
                return BadRequest();
            }

            _context.Entry(lancamentoHoras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LancamentoHorasExists(id))
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

        // POST: api/LancamentoHoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LancamentoHoras>> PostLancamentoHoras(LancamentoHoras lancamentoHoras)
        {
            _context.LancamentoHoras.Add(lancamentoHoras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLancamentoHoras", new { id = lancamentoHoras.Id }, lancamentoHoras);
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

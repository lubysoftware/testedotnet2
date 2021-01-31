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
    public class DesenvolvedorController : ControllerBase
    {
        private readonly Contexto _context;

        public DesenvolvedorController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Desenvolvedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desenvolvedor>>> GetDesenvolvedor()
        {
            return await _context.Desenvolvedor.ToListAsync();
        }

        // GET: api/Desenvolvedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desenvolvedor>> GetDesenvolvedor(int id)
        {
            IQueryable<Desenvolvedor> query = _context.Desenvolvedor;
            query = query.Include(p => p.DesenvolvedoresProjetos);
            query = query.Include(p => p.LancamentosHoras);
            query = query.Where(a => a.Id == id);

            var desenvolvedor = await query.AsNoTracking().OrderBy(q => q.Id).FirstOrDefaultAsync();

            if (desenvolvedor == null)
            {
                return NotFound();
            }

            return desenvolvedor;
        }

        // PUT: api/Desenvolvedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesenvolvedor(int id, Desenvolvedor desenvolvedor)
        {
            if (id != desenvolvedor.Id)
            {
                return BadRequest();
            }

            _context.Entry(desenvolvedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesenvolvedorExists(id))
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

        // POST: api/Desenvolvedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Desenvolvedor>> PostDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            _context.Desenvolvedor.Add(desenvolvedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesenvolvedor", new { id = desenvolvedor.Id }, desenvolvedor);
        }

        // DELETE: api/Desenvolvedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesenvolvedor(int id)
        {
            var desenvolvedor = await _context.Desenvolvedor.FindAsync(id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }

            _context.Desenvolvedor.Remove(desenvolvedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesenvolvedorExists(int id)
        {
            return _context.Desenvolvedor.Any(e => e.Id == id);
        }
    }
}

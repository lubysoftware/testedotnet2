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
    public class LancamentoHorasController : ControllerBase
    {
        private readonly Contexto _context;

        public LancamentoHorasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/LancamentoHoras
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LancamentoHoras>>> GetLancamentoHoras()
        {
            try
            {
                return await _context.LancamentoHoras.ToListAsync();
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao listar as horas cadastradas" });
            }
        }

        // GET: api/LancamentoHoras/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<LancamentoHoras>> GetLancamentoHoras(int id)
        {
            try
            {
                IQueryable<LancamentoHoras> query = _context.LancamentoHoras;
                query = query.Include(p => p.Desenvolvedor);
                query = query.Include(p => p.Projeto);
                query = query.Where(a => a.Id == id);

                var lancamentoHoras = await query.FirstOrDefaultAsync();

                if (lancamentoHoras == null)
                {
                    return Ok(new Validacao("Lançamento de horas não encontrado"));
                }

                return lancamentoHoras;
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao visualizar o lançamento da hora" });
            }
        }

        // POST: api/LancamentoHoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
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
                    return Ok(new Validacao("O projeto não está vinculado ao desenvolvedor"));
                }

                _context.LancamentoHoras.Add(lancamentoHoras);
                await _context.SaveChangesAsync();

                return Ok(new Validacao("Enviado"));
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro no lançamento das Horas" });
            }
        }

        // DELETE: api/LancamentoHoras/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLancamentoHoras(int id)
        {
            try
            {
                var lancamentoHoras = await _context.LancamentoHoras.FindAsync(id);
                if (lancamentoHoras == null)
                {
                    return Ok(new Validacao("Id não foi encontrado"));
                }

                _context.LancamentoHoras.Remove(lancamentoHoras);
                await _context.SaveChangesAsync();

                return Ok(new Validacao("Deletado com sucesso"));
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao deletar a hora cadastrada" });
            }
        }

        private bool LancamentoHorasExists(int id)
        {
            return _context.LancamentoHoras.Any(e => e.Id == id);
        }
    }
}

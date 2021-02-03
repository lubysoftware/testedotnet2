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
    public class ProjetoController : ControllerBase
    {
        private readonly Contexto _context;

        public ProjetoController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Projeto
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetProjeto()
        {
            try
            {
                return await _context.Projeto.ToListAsync();
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao listar projetos" });
            }
        }

        // GET: api/Projeto/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Projeto>> GetProjeto(int id)
        {
            try
            {
                IQueryable<Projeto> query = _context.Projeto;
                query = query.Include(p => p.LancamentosHoras);
                query = query.Include(p => p.DesenvolvedoresProjetos);
                query = query.Where(a => a.Id == id);

                var projeto = await query.FirstOrDefaultAsync();

                if (projeto == null)
                    return Ok(new Validacao("Projeto não encontrado"));

                return projeto;
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao visualizar projeto" });
            }
        }

        // PUT: api/Projeto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProjeto(int id, Projeto projeto)
        {
            if (id != projeto.Id)
            {
                return NotFound(new Validacao("O id na URL está diferente do id do objeto"));
            }

            _context.Entry(projeto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(new Validacao("Atualizado com sucesso"));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoExists(id))
                {
                    return Ok(new Validacao("Projeto inexistente"));
                }
                else
                {
                    return NotFound(new { Erro = "Erro atualizar projeto" });
                }
            }
        }

        // POST: api/Projeto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Projeto>> PostProjeto(Projeto projeto)
        {
            try
            {
                _context.Projeto.Add(projeto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProjeto", new { id = projeto.Id }, projeto);
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao cadastrar projeto" });
            }
        }

        // DELETE: api/Projeto/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            try
            {
                var projeto = await _context.Projeto.FindAsync(id);

                if (projeto == null)
                {
                    return Ok(new Validacao("Id inexistente"));
                }

                _context.Projeto.Remove(projeto);
                await _context.SaveChangesAsync();

                return Ok(new Validacao("Projeto deletado com sucesso"));
            }
            catch (Exception)
            {
                return NotFound(new { Erro = "Erro ao deletar projeto" });
            }
        }

        private bool ProjetoExists(int id)
        {
            return _context.Projeto.Any(e => e.Id == id);
        }
    }
}

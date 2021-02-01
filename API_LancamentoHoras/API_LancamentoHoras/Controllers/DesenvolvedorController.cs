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

        [HttpGet("ValidacaoCPF/{cpf}")]
        public IActionResult ValidacaoCPF(string cpf)
        {
            try
            {
                if (ValidarCPF(cpf))
                    return Ok(new Validacao("Autorizado"));
                else
                    return Ok(new Validacao("Negado"));
            }
            catch (Exception)
            {
                return Ok("Erro na validação do CPF");
            }
        }

        // GET: api/Ranking
        [HttpGet("Ranking")]
        public async Task<ActionResult<IEnumerable<DesenvolvedorRanking>>> GetRanking()
        {
            try
            {
                Semana semana = new Semana(DateTime.Now);

                List<DesenvolvedorRanking> desenvolvedorRankings = new List<DesenvolvedorRanking>();

                var query = await _context.LancamentoHoras
                    .Include(p => p.Desenvolvedor)
                    .Where(x => x.DataInicial >= semana.Inicial && x.DataFinal <= semana.Final).ToListAsync();

                foreach (var item in query.GroupBy(x => x.Desenvolvedor))
                {
                    desenvolvedorRankings.Add(
                        new DesenvolvedorRanking(
                            item.Key.Id,
                            item.Key.Nome,
                            SomaHoras(query.Where(x => x.DesenvolvedorId == item.Key.Id).ToList())
                        )
                    );
                };

                return desenvolvedorRankings.OrderByDescending(x => x.HorasSoma).Take(5).ToList();
            }
            catch (Exception)
            {
                return Ok("Erro ao obter o ranking dos desenvolvedores");
            }
        }

        // GET: api/Desenvolvedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desenvolvedor>>> GetDesenvolvedor()
        {
            try
            {
                return await _context.Desenvolvedor.ToListAsync();
            }
            catch (Exception)
            {

                return Ok("Erro ao obter a lista de desenvolvedores");
            }
        }

        // GET: api/Desenvolvedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desenvolvedor>> GetDesenvolvedor(int id)
        {
            try
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
            catch (Exception)
            {
                return Ok("Erro ao obter os dados do desenvolvedor");
            }            
        }

        // PUT: api/Desenvolvedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesenvolvedor(int id, Desenvolvedor desenvolvedor)
        {
            if (id != desenvolvedor.Id)
            {
                return BadRequest("");
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

        private static TimeSpan SomaHoras(ICollection<LancamentoHoras> lancamentoHoras)
        {
            TimeSpan soma = new TimeSpan();

            foreach (var item in lancamentoHoras)
            {
                TimeSpan sub = item.DataFinal - item.DataInicial;
                soma = soma + sub;
            }

            return soma;
        }

        private static bool ValidarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }

    public class Semana
    {
        public DateTime Inicial { get; set; }
        public DateTime Final { get; set; }

        public Semana(DateTime Data)
        {
            int DiaSemana = (int)Data.DayOfWeek;

            DateTime ini = Data.AddDays(-DiaSemana);
            DateTime fim = Data.AddDays(6 - DiaSemana);

            this.Inicial = new DateTime(ini.Year, ini.Month, ini.Day, 0, 0, 0);
            this.Final = new DateTime(fim.Year, fim.Month, fim.Day, 23, 59, 59);
        }
    }
}

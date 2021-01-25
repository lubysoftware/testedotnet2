using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Desafio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Data.Repository
{
    public class LancamentoHorasRepository : Repository<LancamentoHoras>, ILancamentoHorasRepository
    {
        public LancamentoHorasRepository(DesafioDbContext context) : base(context)
        {

        }


        public async Task<IEnumerable<LancamentoHoras>> ObterLancamentosComDesenvolvedor()
        {
            return await Db.LancamentoHoras.AsNoTracking()
                .Include(d => d.Desenvolvedor)
                .ToListAsync();
        }

    }
}

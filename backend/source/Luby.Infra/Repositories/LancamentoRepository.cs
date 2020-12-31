using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class LancamentoRepository : Repository<Luby.Domain.Models.Lancamento>
    {
        public LancamentoRepository(LubyContext context) : base(context)
        { }

        public override Luby.Domain.Models.Lancamento GetById(int id)
        {
            var query = _context.Set<Luby.Domain.Models.Lancamento>().Where(e => e.Id == id);

            if (query.Any())
            {
                return query.First();
            }
            return null;
        }

        public override IEnumerable<Luby.Domain.Models.Lancamento> GetAll()
        {
            var query = _context.Set<Luby.Domain.Models.Lancamento>();
            return query.Any() ? query.ToList() : new List<Luby.Domain.Models.Lancamento>();
        }
    }
}
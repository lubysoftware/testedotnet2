using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class LancamentoRepository : Repository<Luby.Infra.Context.Lancamento>
    {
        public LancamentoRepository(LubyContext context) : base(context)
        { }

        public override Luby.Infra.Context.Lancamento GetById(int id)
        {
            var query = _context.Set<Luby.Infra.Context.Lancamento>().Where(e => e.Id == id);

            if (query.Any())
            {
                return query.First();
            }
            return null;
        }

        public override IEnumerable<Luby.Infra.Context.Lancamento> GetAll()
        {
            var query = _context.Set<Luby.Infra.Context.Lancamento>();
            return query.Any() ? query.ToList() : new List<Luby.Infra.Context.Lancamento>();
        }
        public override int Save(Luby.Infra.Context.Lancamento lancamento)
        {
            var query = _context.Lancamentos.Add(lancamento);
             return _context.SaveChanges();
             
        }

        public override int Delete(Luby.Infra.Context.Lancamento lancamento)
        {
            var query = _context.Remove(_context.Set<Luby.Infra.Context.Lancamento>().Where(e => e.Id == lancamento.Id));
             return _context.SaveChanges();
            
        }
    }
}
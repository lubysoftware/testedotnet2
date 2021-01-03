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
            var query = _context.Set<Luby.Infra.Context.Lancamento>().Where(e => e.Id == id);

            if (query.Any())
            {
                var first = query.First();
                var result = new Luby.Domain.Models.Lancamento(first.Id, first.DtInicio, first.DtFim, first.IdDesenvolvedor, first.IdProjeto);
                return result;
            }
            return null;
        }

        public override IEnumerable<Luby.Domain.Models.Lancamento> GetAll()
        {
            var query = _context.Set<Luby.Infra.Context.Lancamento>();

            var result = new List<Luby.Domain.Models.Lancamento>();
            if (query.Any())
            {
                foreach (var item in query)
                {
                    var projeto = new Luby.Domain.Models.Lancamento(item.Id, item.DtInicio, item.DtFim, item.IdDesenvolvedor, item.IdProjeto);
                    result.Add(projeto);
                }
                return result;
            }
            else return new List<Luby.Domain.Models.Lancamento>();
        }
        public override int Save(Luby.Domain.Models.Lancamento lancamento)
        {
            var lanc = new Luby.Infra.Context.Lancamento()
            {
                DtFim = lancamento.DtFim,
                DtInicio = lancamento.DtInicio,
                IdDesenvolvedor = lancamento.IdDesenvolvedor,
                IdProjeto = lancamento.IdProjeto,
            };
            var query = _context.Lancamentos.Add(lanc);
            return _context.SaveChanges();
        }

        public override int Delete(Luby.Domain.Models.Lancamento lancamento)
        {
            var query = _context.Remove(_context.Set<Luby.Infra.Context.Lancamento>().Where(e => e.Id == lancamento.Id));
            return _context.SaveChanges();

        }
    }
}
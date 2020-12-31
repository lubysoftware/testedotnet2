using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class ProjetoRepository : Repository<Luby.Domain.Models.Projeto>
    {

        public ProjetoRepository(LubyContext context) : base(context)
        {}
        public override Luby.Domain.Models.Projeto GetById(int id)
        {
            var query = _context.Set<Luby.Domain.Models.Projeto>().Where(e => e.Id == id);

            if (query.Any())
            {
                return query.First();
            }
            return null;
        }

        public override IEnumerable<Luby.Domain.Models.Projeto> GetAll()
        {
            var query = _context.Set<Luby.Domain.Models.Projeto>();
            return query.Any() ? query.ToList() : new List<Luby.Domain.Models.Projeto>();
        }
    }
}
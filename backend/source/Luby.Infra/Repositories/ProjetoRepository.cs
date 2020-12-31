using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class ProjetoRepository : Repository<Luby.Infra.Context.Projeto>
    {

        public ProjetoRepository(LubyContext context) : base(context)
        {}
        public override Luby.Infra.Context.Projeto GetById(int id)
        {
            var query = _context.Set<Luby.Infra.Context.Projeto>().Where(e => e.Id == id);

            if (query.Any())
            {
                return query.First();
            }
            return null;
        }

        public override IEnumerable<Luby.Infra.Context.Projeto> GetAll()
        {
            var query = _context.Set<Luby.Infra.Context.Projeto>();
            return query.Any() ? query.ToList() : new List<Luby.Infra.Context.Projeto>();
        }

         public override int Save(Luby.Infra.Context.Projeto projeto ){
            var query = _context.Projetos.Add(projeto);
            return _context.SaveChanges();
             
        }
    }
}
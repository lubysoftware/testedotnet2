using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class ProjetoRepository : Repository<Luby.Domain.Models.Projeto>
    {

        public ProjetoRepository(LubyContext context) : base(context)
        { }
        public override Luby.Domain.Models.Projeto GetById(int id)
        {
            var query = _context.Set<Luby.Infra.Context.Projeto>().Where(e => e.Id == id);

            if (query.Any())
            {
                var first = query.First();
                var result = new Luby.Domain.Models.Projeto(first.Nome);
                return result;
            }
            return null;
        }

        public override IEnumerable<Luby.Domain.Models.Projeto> GetAll()
        {
            var query = _context.Set<Luby.Infra.Context.Projeto>();
            var result = new List<Luby.Domain.Models.Projeto>();
            if (query.Any())
            {
                foreach (var item in query)
                {
                    var projeto = new Luby.Domain.Models.Projeto(item.Nome);
                    result.Add(projeto);
                }
                return result;
            }
            else return new List<Luby.Domain.Models.Projeto>();
        }

        public override int Save(Luby.Domain.Models.Projeto projeto)
        {
            var prj =new Luby.Infra.Context.Projeto(){
                Id=projeto.Id,
                Nome=projeto.Nome
            };
            var query = _context.Projetos.Add(prj);
            return _context.SaveChanges();

        }
    }
}
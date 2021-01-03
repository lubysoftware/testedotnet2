using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;
namespace Luby.Infra.Repositories
{
    public class DesenvolvedorRepository : Repository<Luby.Domain.Models.Desenvolvedor>
    {
        public DesenvolvedorRepository(LubyContext context) : base(context)
        { }

        public override Luby.Domain.Models.Desenvolvedor GetById(int id)
        {
            var query = _context.Set<Luby.Infra.Context.Desenvolvedor>().Where(e => e.Id == id);

            if (query.Any())
            {
                //   return query.First();
                var first = query.First();
                var result = new Luby.Domain.Models.Desenvolvedor(first.Nome,
                first.Cpf, first.Cargo, first.Email, first.Login, first.Senha);
                return result;
            }
            return null;
        }

        public override IEnumerable<Luby.Domain.Models.Desenvolvedor> GetAll()
        {
            var query = _context.Set<Luby.Infra.Context.Desenvolvedor>();
            var result = new List<Luby.Domain.Models.Desenvolvedor>();
            if (query.Any())
            {
                foreach (var item in query)
                {
                    var projeto = new Luby.Domain.Models.Desenvolvedor(item.Nome,item.Cpf,item.Cargo, item.Email,item.Login,item.Senha);
                    result.Add(projeto);
                }
                return result;
            }
            else return new List<Luby.Domain.Models.Desenvolvedor>();
        }
        public override int Save(Luby.Domain.Models.Desenvolvedor desenvolvedor)
        {
            var dev = new Luby.Infra.Context.Desenvolvedor()
            {
                Cargo = desenvolvedor.Cargo,
                Cpf = desenvolvedor.Cpf,
                Email = desenvolvedor.Email,
                Id = desenvolvedor.Id,
                Login = desenvolvedor.Login,
                Nome = desenvolvedor.Nome,
                Senha = desenvolvedor.Senha

            };
            var query = _context.Desenvolvedors.Add(dev);
            return _context.SaveChanges();
        }

        public override int Delete(Luby.Domain.Models.Desenvolvedor desenvolvedor)
        {
            var query = _context.Remove(_context.Set<Luby.Infra.Context.Desenvolvedor>().Where(e => e.Id == desenvolvedor.Id));
            return _context.SaveChanges();

        }
    }
}
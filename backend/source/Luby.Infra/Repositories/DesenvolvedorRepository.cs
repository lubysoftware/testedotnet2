using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;
namespace Luby.Infra.Repositories
{
    public class DesenvolvedorRepository : Repository<Luby.Infra.Context.Desenvolvedor>
    {
        public DesenvolvedorRepository(LubyContext context) : base(context)
        { }

        public override Luby.Infra.Context.Desenvolvedor GetById(int id)
        {
            var query = _context.Set<Luby.Infra.Context.Desenvolvedor>().Where(e => e.Id == id);

            if (query.Any())
            {
                return query.First();
            }
            return null;
        }

        public override IEnumerable<Luby.Infra.Context.Desenvolvedor> GetAll()
        {
            var query = _context.Set<Luby.Infra.Context.Desenvolvedor>();
            return query.Any() ? query.ToList() : new List<Luby.Infra.Context.Desenvolvedor>();
        }
        public override int Save(Luby.Infra.Context.Desenvolvedor desenvolvedor)
        {
            var query = _context.Desenvolvedors.Add(desenvolvedor);
            return _context.SaveChanges();
        }

        public override int Delete(Luby.Infra.Context.Desenvolvedor desenvolvedor)
        {
            var query = _context.Remove(_context.Set<Luby.Infra.Context.Desenvolvedor>().Where(e => e.Id == desenvolvedor.Id));
            return _context.SaveChanges();

        }

        private  Luby.Domain.Models.Desenvolvedor ConverterParaDominio(Luby.Infra.Context.Desenvolvedor infra)
        {
            try
            {
                var dominio = new Luby.Domain.Models.Desenvolvedor(
                    infra.Nome,
                    infra.Cpf,
                    infra.Cargo,
                    infra.Email,
                    infra.Login,
                    infra.Senha);
                return dominio;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private  Luby.Infra.Context.Desenvolvedor ConverterParaModelo(Luby.Domain.Models.Desenvolvedor modelo)
        {
            try
            {
                var infra = new Luby.Infra.Context.Desenvolvedor();
                infra.Nome = modelo.Nome;
                infra.Cpf = modelo.Cpf;
                infra.Cargo = modelo.Cargo;
                infra.Email = modelo.Email;
                infra.Login = modelo.Login;
                infra.Senha = modelo.Senha;
                return infra;
            }
            catch (System.Exception)
            {
                return new Luby.Infra.Context.Desenvolvedor();
            }
        }
    }
}
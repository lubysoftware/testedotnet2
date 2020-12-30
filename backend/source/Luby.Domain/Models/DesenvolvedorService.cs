using Luby.Domain.Interfaces;

namespace Luby.Domain.Models
{
    public class DesenvolvedorService
    {
        private readonly IRepository<Desenvolvedor> _desenvolvedorRepository;

        public DesenvolvedorService(IRepository<Desenvolvedor> desenvolvedorRepository)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
        }

        public void Save(int id, string nome, string cargo, string email, string login, string senha)
        {
            var desenvolvedor = _desenvolvedorRepository.GetById(id);

            if (desenvolvedor == null)
            {
                desenvolvedor = new Desenvolvedor(nome, cargo, email, login, senha);
                _desenvolvedorRepository.Save(desenvolvedor);
            }
            else
                desenvolvedor.Update(nome, cargo, email, login, senha);
        }
    }
}
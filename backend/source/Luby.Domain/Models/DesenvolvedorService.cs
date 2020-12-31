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

        public bool Save(int id, string nome, string cpf,string cargo, string email, string login, string senha)
        {
            var desenvolvedor = _desenvolvedorRepository.GetById(id);

            if (desenvolvedor == null)
            {
                desenvolvedor = new Desenvolvedor(nome,cpf, cargo, email, login, senha);
                try
                {
                    _desenvolvedorRepository.Save(desenvolvedor);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
                
            }
            else
                return true;//desenvolvedor.Update(nome, cpf,cargo, email, login, senha);
        }

        public bool Delete (int id){
              var desenvolvedor = _desenvolvedorRepository.GetById(id);

            if (desenvolvedor != null)
            {
                try
                {
                 _desenvolvedorRepository.Delete(desenvolvedor);
                 return true;   
                }
                catch (System.Exception)
                {
                    return false;
                }
                 
            }
            else
                return false;
        }
            
    }
}
using Luby.Domain.Interfaces;

namespace Luby.Domain.Models
{
    public class ProjetoService
    {
        private readonly IRepository<Projeto> _projetoRepository;

        public ProjetoService(IRepository<Projeto> projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public void Save(int id,string nome){
            var projeto = _projetoRepository.GetById(id);

            if (projeto == null)
            {
                projeto = new Projeto(nome);
                _projetoRepository.Save(projeto);
            }
            else
                projeto.Update(nome);
        }
    }
}
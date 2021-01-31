using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Desenvolvedor;

namespace Apihorasdesenvolvedor.Servico.Servicos
{
    public class DesenvolvedorService : IDesenvolvedorService
    {
        private IRepositorio<DesenvolvedorEntity> _repository;

        public DesenvolvedorService(IRepositorio<DesenvolvedorEntity> repositorio)
        {
            _repository = repositorio;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DesenvolvedorEntity> Get(int id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<DesenvolvedorEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<DesenvolvedorEntity> Post(DesenvolvedorEntity desenvolvedor)
        {
            return await _repository.InsertAsync(desenvolvedor);
        }

        public async Task<DesenvolvedorEntity> Put(DesenvolvedorEntity desenvolvedor)
        {
            return await _repository.UpdateAsync(desenvolvedor);
        }
    }
}

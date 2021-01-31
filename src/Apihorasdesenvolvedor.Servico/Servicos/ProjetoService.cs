using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Projeto;

namespace Apihorasdesenvolvedor.Servico.Servicos
{
    public class ProjetoService : IProjetoService
    {
        private IRepositorio<ProjetoEntity> _repository;

        public ProjetoService(IRepositorio<ProjetoEntity> repositorio)
        {
            _repository = repositorio;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ProjetoEntity> Get(int id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<ProjetoEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<ProjetoEntity> Post(ProjetoEntity projeto)
        {
            return await _repository.InsertAsync(projeto);
        }

        public async Task<ProjetoEntity> Put(ProjetoEntity projeto)
        {
            return await _repository.UpdateAsync(projeto);
        }
    }
}

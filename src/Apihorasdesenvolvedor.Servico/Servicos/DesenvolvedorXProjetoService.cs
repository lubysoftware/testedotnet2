using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXProjeto;

namespace Apihorasdesenvolvedor.Servico.Servicos
{
    public class DesenvolvedorXProjetoService : IDesenvolvedorXProjetoService
    {
        private IRepositorio<DesenvolvedorXProjetoEntity> _repository;
        public DesenvolvedorXProjetoService(IRepositorio<DesenvolvedorXProjetoEntity> repositorio)
        {
            _repository = repositorio;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DesenvolvedorXProjetoEntity> Get(int id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<DesenvolvedorXProjetoEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<DesenvolvedorXProjetoEntity> Post(DesenvolvedorXProjetoEntity desenvolvedorXProjeto)
        {
            return await _repository.InsertAsync(desenvolvedorXProjeto);
        }

        public async Task<DesenvolvedorXProjetoEntity> Put(DesenvolvedorXProjetoEntity desenvolvedorXProjeto)
        {
            return await _repository.UpdateAsync(desenvolvedorXProjeto);
        }
    }
}

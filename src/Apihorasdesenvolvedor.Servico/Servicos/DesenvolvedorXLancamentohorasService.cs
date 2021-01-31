using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;
using Apihorasdesenvolvedor.Dominio.Interfaces;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXLancamentohoras;
using Apihorasdesenvolvedor.Servico.Servicos.BO;

namespace Apihorasdesenvolvedor.Servico.Servicos
{
    public class DesenvolvedorXLancamentohorasService : IDesenvolvedorXLancamentohorasService
    {
        private IRepositorio<DesenvolvedorXLancamentohorasEntity> _repository;
        public DesenvolvedorXLancamentohorasService(IRepositorio<DesenvolvedorXLancamentohorasEntity> repositorio)
        {
            _repository = repositorio;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DesenvolvedorXLancamentohorasEntity> Get(int id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<DesenvolvedorXLancamentohorasEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<DesenvolvedorXLancamentohorasEntity> Post(DesenvolvedorXLancamentohorasEntity desenvolvedorXLancamentohoras)
        {
            return await _repository.InsertAsync(desenvolvedorXLancamentohoras);
        }

        public async Task<DesenvolvedorXLancamentohorasEntity> Put(DesenvolvedorXLancamentohorasEntity desenvolvedorXLancamentohoras)
        {
            return await _repository.UpdateAsync(desenvolvedorXLancamentohoras);
        }


        public async Task<IEnumerable<DesenvolvedorXLancamentohorasEntity>> GetFiveTop()
        {
           RelatorioHoras novorelatorio = new RelatorioHoras(_repository);

            var implementaraquiobo  = novorelatorio.TopFiveDesenvolvedoresAsync();

            return await _repository.SelectAsync();
        }        

    }

}

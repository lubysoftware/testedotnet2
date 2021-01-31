using System.Collections.Generic;
using System.Threading.Tasks;
using Apihorasdesenvolvedor.Dominio.Entidades;

namespace Apihorasdesenvolvedor.Dominio.Interfaces
{
    public interface IRepositorio<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<T> SelectAsync(int id);
        Task<IEnumerable<T>> SelectAsync();

    }
}

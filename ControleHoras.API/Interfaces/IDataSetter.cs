using System.Threading.Tasks;

namespace ControleHoras.API.Interfaces
{
    interface IDataSetter<T>
    {
        public T Insert(T obj);
    }
}
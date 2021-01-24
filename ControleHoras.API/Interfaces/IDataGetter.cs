using System.Collections.Generic;

namespace ControleHoras.API.Interfaces
{
    internal interface IDataGetter<T>
    {
        public T ById(int id);
        public IList<T> PagedGet(int skip, int take);
    }
}
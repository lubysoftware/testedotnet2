using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Persistencia.DAL
{
    public interface IDAO<Entidade>:IDisposable
    {
        void Add<E>(E obj) where E : class, Entidade;
        void Update<E>(E novo, int id) where E : class, Entidade;
        void Delete<E>(E obj) where E : class, Entidade;
        IDbContextTransaction BeginTtransaction();
        void Save();
     }
}

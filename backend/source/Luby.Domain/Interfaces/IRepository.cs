using System.Collections.Generic;
using System.Collections;

namespace Luby.Domain.Interfaces
{
    public interface IRepository<TEntity>where TEntity:class
    {
         TEntity GetById(int id);
         IEnumerable <TEntity>GetAll();
         int Save(TEntity entity);
         int Delete(TEntity entity);
  
    }
}
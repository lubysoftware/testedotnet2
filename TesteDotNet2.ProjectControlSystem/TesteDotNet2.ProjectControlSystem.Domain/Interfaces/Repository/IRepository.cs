using System;
using System.Collections.Generic;

namespace TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity GetById(Guid id);
        List<TEntity> Get(int page, int size);
        TEntity Update(TEntity obj);
        bool Delete(Guid id);
    }
}

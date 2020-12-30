using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Interfaces;
using Luby.Domain.Models;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly LubyContext _context;

        public Repository(LubyContext context)
        {
            _context = context;
        }

        public virtual TEntity GetById(int id)
        {
            var query = _context.Set<TEntity>().Where(e=>e.Id == id);
            if (query.Any())
            {
                return query.FirstOrDefault();
            }
            return null;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = _context.Set<TEntity>();

            if (query.Any())
            {
                return query.ToList();
            }

            return new List<TEntity>();
        }

        public virtual void Save(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
    }
}
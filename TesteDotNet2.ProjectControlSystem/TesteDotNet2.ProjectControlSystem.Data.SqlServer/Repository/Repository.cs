using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ProjectControlSystemContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(ProjectControlSystemContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            var objreturn = DbSet.Add(obj);
            var result = Db.SaveChanges();
            return objreturn.Entity;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var entity = DbSet.Remove(DbSet.Find(id));
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;                  
            }
        }

        public List<TEntity> Get(int page, int size)
        {
            return DbSet.Skip((page - 1) * size).Take(size).ToList();
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public TEntity Update(TEntity obj)
        {
            var objResult = DbSet.Update(obj);
            var result = Db.SaveChanges();
            return objResult.Entity;
        }
    }
}

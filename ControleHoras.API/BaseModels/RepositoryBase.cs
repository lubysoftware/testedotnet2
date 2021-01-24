using ControleHoras.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ControleHoras.API.BaseModels
{
    public abstract class RepositoryBase<T>
    {
        public static T Instance { get; private set; } = (T)Activator.CreateInstance(typeof(T));

        internal IQueryable<TT> FindById<TT>(DbSet<TT> obj, int id) where TT : class
        {
            var list = obj.Cast<EntityModelBase>();
            var result = list.Where(x => x.Id == id);
            return result.Cast<TT>(); ;
        }
        internal IQueryable<TT> SkipTake<TT>(DbSet<TT> dbSet, int skip, int take) where TT : class
        {
            var dbsetCast = dbSet.Cast<EntityModelBase>();

            var result = dbsetCast.OrderBy(x => x).Skip(skip).Take(take);

            return result.Cast<TT>();
        }
        internal TT InsertOnTable<TT>(DbSet<TT> dbSet, TT obj) where TT : class
        {
            var result = dbSet.Add(obj);

            // Database Context of .Add() and .SaveChanged() have to be the same!
            DbContextGetter.GetDbContext(dbSet).SaveChanges();

            result.GetDatabaseValues();
            return result.Entity;
        }
    }
}

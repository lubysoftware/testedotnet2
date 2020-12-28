using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Reflection;

namespace Persistencia.DAL
{
    public abstract class GenericDAO<Entidade> : IDAO<Entidade>
    {
        protected readonly Context _context;

        public GenericDAO(Context context)
        {
            _context = context;
        }
        public abstract Entidade[] GetAll();
        public abstract Entidade[] List(int items,int page);
        
        public abstract Entidade GetById(int id);
        public virtual int Total() {
            return GetAll().Length;
        }

        public virtual void Add<E>(E obj) where E : class, Entidade
        {
            try
            {
                _context.Set<E>().Add(obj);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public virtual void Update<E>(E changed,int id) where E : class, Entidade
        {
            try
            {
                E old = GetById(id) as E;
                _context.Entry<E>(old).CurrentValues.SetValues(changed);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public virtual void Delete<E>(E obj) where E : class, Entidade
        {
            try
            {
                _context.Set<E>().Remove(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Save()
        {
            try {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Dispose(){}

        public IDbContextTransaction BeginTtransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}


      
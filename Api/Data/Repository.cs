using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDotnet.Data
{
    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}

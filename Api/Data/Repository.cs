using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public Developer[] GetAllDevelopers()
        {
            IQueryable<Developer> query = _context.Developer;

            query = query.AsNoTracking().OrderBy(d => d.Id);
            return query.ToArray();
        }

        public Entry[] GetAllEntries()
        {
            IQueryable<Entry> query = _context.Entry;

            query = query.AsNoTracking().OrderBy(d => d.Id);
            return query.ToArray();
        }

        public Project[] GetAllProjects()
        {
            IQueryable<Project> query = _context.Project;

            query = query.AsNoTracking().OrderBy(d => d.Id);
            return query.ToArray();
        }

        public Developer GetDeveloperById(int developerId)
        {
            IQueryable<Developer> query = _context.Developer;

            query = query.AsNoTracking().OrderBy(d => d.Id)
                .Where(dev => dev.Id == developerId);

            return query.FirstOrDefault();
        }

        public Entry GetEntryById(int entryId)
        {
            IQueryable<Entry> query = _context.Entry;

            query = query.AsNoTracking().OrderBy(d => d.Id)
                .Where(prj => prj.Id == entryId);

            return query.FirstOrDefault();
        }

        public Project GetProjectById(int projectId)
        {
            IQueryable<Project> query = _context.Project;

            query = query.AsNoTracking().OrderBy(d => d.Id)
                .Where(prj => prj.Id == projectId);

            return query.FirstOrDefault();
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

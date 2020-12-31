using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteDotnet.Models.ViewModels;

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

            query = query.Include(d => d.DeveloperProject)
                .ThenInclude(dp => dp.Project);

            query = query.Include(d => d.Entries);

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

        public Developer GetDeveloperLogin(string email, string password)
        {
            IQueryable<Developer> query = _context.Developer;

            query = query.AsNoTracking().OrderBy(dev => dev.Email)
                .Where(dev => dev.Email == email && dev.Password == password);

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

        public bool IsDateAvailable(Entry entry)
        {
            IQueryable<Entry> query = _context.Entry;

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.DeveloperId == entry.DeveloperId)
                .Where(e => 
                e.InitialDate <= entry.InitialDate
                && entry.InitialDate <= e.EndDate
                || e.InitialDate <= entry.EndDate
                && entry.EndDate <= e.EndDate);

            Entry queryEntry = query.FirstOrDefault();
            return queryEntry == null;
        }

        public bool DeveloperHasProject(int developerId, int projectId)
        {
            IQueryable<DeveloperProject> query = _context.DeveloperProject;

            query = query.AsNoTracking().OrderBy(dp => dp.DeveloperId)
                .Where(devPrj => devPrj.DeveloperId == developerId && devPrj.ProjectId == projectId);

            return query.FirstOrDefault() != null;
        }

        public WorkedHoursRank[] GetDeveloperRank()
        {
            string stringQuery =
                "SELECT TOP(5) [DeveloperId], SUM(DATEDIFF(HOUR, [InitialDate], [EndDate])) AS WorkedHours\n" +
                "FROM [dbo].[Entry]\n" +
                "WHERE [InitialDate] >= DATEADD(day, -7, GETDATE())\n" +
                "GROUP BY [DeveloperId];";

            return _context.WorkedHoursRank.FromSqlRaw(stringQuery).ToArray();
        }
    }
}

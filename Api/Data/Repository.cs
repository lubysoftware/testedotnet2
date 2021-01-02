using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TesteDotnet.Helpers;
using TesteDotnet.Models.ViewModels;
using TesteDotnet.V1.Dtos;

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

        public async Task<PageList<Developer>> GetAllDevelopersAsync(PageParams pageParams)
        {
            IQueryable<Developer> query = _context.Developer;

            query = query.Include(d => d.DeveloperProject)
                .ThenInclude(dp => dp.Project);

            query = query.Include(d => d.Entries);

            query = query.AsNoTracking().OrderBy(d => d.Id);

            return await PageList<Developer>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Entry[]> GetAllEntriesAsync()
        {
            IQueryable<Entry> query = _context.Entry;

            query = query.AsNoTracking().OrderBy(d => d.Id);

            return await query.ToArrayAsync();
        }

        public async Task<PageList<Project>> GetAllProjectsAsync(PageParams pageParams)
        {
            IQueryable<Project> query = _context.Project;

            query = query.AsNoTracking().OrderBy(d => d.Id);

            return await PageList<Project>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Developer> GetDeveloperByIdAsync(int developerId)
        {
            IQueryable<Developer> query = _context.Developer;

            query = query.AsNoTracking().OrderBy(d => d.Id)
                .Where(dev => dev.Id == developerId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Entry> GetEntryByIdAsync(int entryId)
        {
            IQueryable<Entry> query = _context.Entry;

            query = query.AsNoTracking().OrderBy(d => d.Id)
                .Where(prj => prj.Id == entryId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Developer> GetDeveloperLoginAsync(string email, string password)
        {
            IQueryable<Developer> query = _context.Developer;

            query = query.AsNoTracking().OrderBy(dev => dev.Email)
                .Where(dev => dev.Email == email && dev.Password == password);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            IQueryable<Project> query = _context.Project;

            query = query.AsNoTracking().OrderBy(d => d.Id)
                .Where(prj => prj.Id == projectId);

            return await query.FirstOrDefaultAsync();
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

        public async Task<WorkedHoursRank[]> GetDeveloperRankAsync()
        {
            string stringQuery =
                "SELECT TOP(5) [DeveloperId], [Name] AS DeveloperName, SUM(DATEDIFF(HOUR, [InitialDate], [EndDate])) AS WorkedHours\n" +
                "FROM [dbo].[Entry]\n" +
                "LEFT JOIN[dbo].[Developer]\n" +
                "ON[dbo].[Entry].[DeveloperId] = [dbo].[Developer].[Id]\n" +
                "WHERE [InitialDate] >= DATEADD(day, -7, GETDATE())\n" +
                "GROUP BY [DeveloperId], [Name]\n" +
                "ORDER BY WorkedHours DESC;";

            return await _context.WorkedHoursRank.FromSqlRaw(stringQuery).ToArrayAsync();
        }
    }
}

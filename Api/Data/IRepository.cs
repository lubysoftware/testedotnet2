using Api.Models;
using System.Threading.Tasks;
using TesteDotnet.Helpers;
using TesteDotnet.Models.ViewModels;
using TesteDotnet.V1.Dtos;

namespace TesteDotnet.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool SaveChanges();

        Task<PageList<Developer>> GetAllDevelopersAsync(PageParams pageParams);

        Task<Developer> GetDeveloperByIdAsync(int developerId);

        Task<Developer> GetDeveloperLoginAsync(string email, string password);

        Task<WorkedHoursRank[]> GetDeveloperRankAsync();

        Task<PageList<Project>> GetAllProjectsAsync(PageParams pageParams);

        Task<Project> GetProjectByIdAsync(int projectId);

        Task<Entry[]> GetAllEntriesAsync();

        Task<Entry> GetEntryByIdAsync(int entryId);

        bool IsDateAvailable(Entry entry);

        bool DeveloperHasProject(int developerId, int projectId);
    }
}

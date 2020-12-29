using Api.Models;

namespace TesteDotnet.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool SaveChanges();

        Developer[] GetAllDevelopers();

        Developer GetDeveloperById(int developerId);

        Project[] GetAllProjects();

        Project GetProjectById(int projectId);

        Entry[] GetAllEntries();

        Entry GetEntryById(int entryId);

    }
}

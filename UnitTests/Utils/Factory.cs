using Api.Models;
using System.Threading.Tasks;
using TesteDotnet.Data;
using Brazil.Data;

namespace UnitTests.Utils
{
    public class Factory
    {
        private readonly Context _context;
        private CpfGenerator _cpfGenerator;

        public Factory(Context context)
        {
            _context = context;
            _cpfGenerator = new CpfGenerator();
        }

        public async Task<Developer> CreateDeveloper()
        {
            var developer = new Developer();
            developer.Name = new Bogus.DataSets.Name().FullName();
            developer.Email = new Bogus.DataSets.Internet().Email(developer.Name);
            developer.CPF = _cpfGenerator.Next().ToString().RemoveNonNumeric();
            developer.Password = new Bogus.DataSets.Internet().Password();

            await _context.Developer.AddAsync(developer);
            await _context.SaveChangesAsync();

            return developer;
        }

        public async Task<Project> CreateProject()
        {
            var project = new Project();
            project.Name = new Bogus.DataSets.Name().JobArea();
            await _context.Project.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Entry> CreateEntry(int projectId, int developerId)
        {
            var entry = new Entry();
            entry.InitialDate = new Bogus.DataSets.Date().Recent();
            entry.EndDate = new Bogus.DataSets.Date().Soon();
            entry.DeveloperId = developerId;
            entry.ProjectId = projectId;

            await _context.Entry.AddAsync(entry);
            await _context.SaveChangesAsync();

            return entry;
        }
    }

}

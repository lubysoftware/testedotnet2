using System;
using System.Threading.Tasks;
using Tasks.Domain._Common.Results;
using Tasks.Domain.Developers.Repositories;
using Tasks.Domain.External.Services;
using Tasks.Domain.Projects.Repositories;
using Tasks.Domain.Works.Dtos;
using Tasks.Domain.Works.Repositories;
using Tasks.Domain.Works.Services;

namespace Tasks.Services.Works
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _workRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMockyService _mockyService;

        public WorkService(
            IWorkRepository workRepository,
            IDeveloperRepository developerRepository,
            IProjectRepository projectRepository,
            IMockyService mockyService
        ) {
            _workRepository = workRepository;
            _developerRepository = developerRepository;
            _projectRepository = projectRepository;
            _mockyService = mockyService;
        }

        public Task<Result> CreateWorkAsync(WorkCreateDto workDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteWorkAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateWorkAsync(WorkUpdateDto workDto)
        {
            throw new NotImplementedException();
        }
    }
}

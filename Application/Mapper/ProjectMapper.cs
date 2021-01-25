using AutoMapper;
using Domain.Entities;
using DTO.Request;
using DTO.Response;

namespace Application.Mapper
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<Project, ProjectRequestDTO>();
            CreateMap<ProjectRequestDTO, Project>();

            CreateMap<Project, ProjectResponseDTO>();
        }

    }
}

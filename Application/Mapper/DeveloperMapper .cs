using AutoMapper;
using Domain.Entities;
using DTO.Request;
using DTO.Response;

namespace Application.Mapper
{
    public class DeveloperMapper : Profile
    {
        public DeveloperMapper()
        {
            CreateMap<Developer, DeveloperRequestDTO>();
            CreateMap<DeveloperRequestDTO, Developer>();

            CreateMap<Developer, DeveloperResponseDTO>();
        }

    }
}

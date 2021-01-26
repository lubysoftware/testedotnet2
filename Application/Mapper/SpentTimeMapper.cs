using AutoMapper;
using Domain.Entities;
using DTO.Request;
using DTO.Response;

namespace Application.Mapper
{
    public class SpentTimeMapper : Profile
    {
        public SpentTimeMapper()
        {
            CreateMap<SpentTime, SpentTimeRequestDTO>();
            CreateMap<SpentTimeRequestDTO, SpentTime>();

            CreateMap<SpentTime, SpentTimeResponseDTO>();
        }

    }
}

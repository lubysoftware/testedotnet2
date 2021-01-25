using AutoMapper;
using Desafio.API.Models;
using Desafio.Business.Models;

namespace Desafio.API.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Desenvolvedor, DesenvolvedorViewModel>().ReverseMap();
            CreateMap<Desenvolvedor, DesenvolvedorAdicionarViewModel>().ReverseMap();
            CreateMap<Projeto, ProjetoViewModel>().ReverseMap();
            CreateMap<LancamentoHoras, LancamentoHorasViewModel>().ReverseMap();
        }
    }
}

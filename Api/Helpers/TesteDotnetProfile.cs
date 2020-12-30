using Api.Models;
using AutoMapper;
using TesteDotnet.Dtos;

namespace TesteDotnet.Helpers
{
    public class TesteDotnetProfile : Profile
    {
        public TesteDotnetProfile()
        {
            CreateMap<Developer, DeveloperDto>();
        }
    }
}

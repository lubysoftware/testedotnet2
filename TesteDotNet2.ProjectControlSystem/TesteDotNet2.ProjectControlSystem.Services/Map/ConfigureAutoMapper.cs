
using AutoMapper;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Services.ViewModel;

namespace TesteDotNet2.ProjectControlSystem.Services.Map
{
    public class ConfigureAutoMapper : Profile
    {
        public ConfigureAutoMapper()
        {
            CreateMap<Developer, DeveloperViewModel>().ReverseMap();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<TimeSheet, TimeSheetViewModel>().ReverseMap();
            CreateMap<ReportDeveloperResponse, ReportDeveloperResponseViewModel>().ReverseMap();
        }
    }
}

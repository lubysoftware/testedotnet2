﻿using Api.Models;
using AutoMapper;
using TesteDotnet.V1.Dtos;

namespace TesteDotnet.V1.Profiles
{
    public class TesteDotnetProfile : Profile
    {
        public TesteDotnetProfile()
        {
            CreateMap<Developer, DeveloperDto>();
            CreateMap<Entry, EntryDto>();
            CreateMap<Project, ProjectDto>();
        }
    }
}

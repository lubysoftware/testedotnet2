using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Application.Mapper;
using System;

namespace Application.IoC.Mappers
{
    public static class MappersIoC
    {
        public static void ApplicationMappersIoC(this IServiceCollection services, Type type)
        {
            services.AddAutoMapper(x =>
            {
                x.AddProfile(new DeveloperMapper());
                x.AddProfile(new ProjectMapper());
            }, type);
        }
    }
}

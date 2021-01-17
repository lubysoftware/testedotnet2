using AutoMapper;
using luby_app.Application.Common.Mappings;
using System;

namespace luby_app.Application.DesenvolvedorHoras.Queries
{
    public class DesenvolvedorHoraDto : IMapFrom<Domain.Entities.DesenvolvedorHora>
    {
        public int Id { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public int DesenvolvedorId { get; set; }

        public Desenvolvedor.Queries.GetDesenvolvedorWithPagination.DesenvolvedorDto Desenvolvedor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.DesenvolvedorHora, DesenvolvedorHoraDto>();
        }
    }
}

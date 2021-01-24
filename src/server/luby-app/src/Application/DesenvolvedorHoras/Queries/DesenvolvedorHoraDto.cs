using AutoMapper;
using luby_app.Application.Common.Mappings;
using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;
using luby_app.Application.Projeto.Queries.GetProjetosWithPagination;
using System;

namespace luby_app.Application.DesenvolvedorHoras.Queries
{
    public class DesenvolvedorHoraDto : IMapFrom<Domain.Entities.DesenvolvedorHora>
    {
        public int Id { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public int DesenvolvedorId { get; set; }

        public DesenvolvedorDto Desenvolvedor { get; set; }

        public int ProjetoId { get; set; }

        public ProjetoDto Projeto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.DesenvolvedorHora, DesenvolvedorHoraDto>();
        }
    }
}

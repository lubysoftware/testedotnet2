using AutoMapper;
using luby_app.Application.Common.Mappings;

namespace luby_app.Application.Projeto.Queries.GetProjetosWithPagination
{
    public class ProjetoDto : IMapFrom<Domain.Entities.Projeto>
    {
        public int Id { get; set; }

        public string Nome { get; set; }
         
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Projeto, ProjetoDto>() ;
        }
    }
}

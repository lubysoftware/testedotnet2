using AutoMapper;
using luby_app.Application.Common.Mappings;
using luby_app.Application.Projeto.Queries.GetProjetosWithPagination;
using System.Text.Json.Serialization;

namespace luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination
{
    public class DesenvolvedorDto : IMapFrom<Domain.Entities.Desenvolvedor>
    {
        public int Id { get; set; }

        public string Nome { get; set; }    

        public string Email { get; set; }

        [JsonIgnore]
        public string Senha { get; set; }

        public string CPF { get; set; }

        public int ProjetoId { get; set; }

        public double TotalHoras { get; set; }

        public ProjetoDto Projeto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Desenvolvedor, DesenvolvedorDto>();
        }
    }
}

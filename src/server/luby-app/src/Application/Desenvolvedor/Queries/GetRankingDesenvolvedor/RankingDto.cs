using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;
using luby_app.Application.Projeto.Queries.GetProjetosWithPagination;

namespace luby_app.Application.Desenvolvedor.Queries.GetRankingDesenvolvedor
{
    public class RankingDto
    {
        public double MediaHoras { get; set; } 

        public DesenvolvedorDto Desenvolvedor { get; set; }

        public ProjetoDto Projeto { get; set; }
    }
}

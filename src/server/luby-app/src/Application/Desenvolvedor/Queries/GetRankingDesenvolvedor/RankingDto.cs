using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;

namespace luby_app.Application.Desenvolvedor.Queries.GetRankingDesenvolvedor
{
    public class RankingDto
    {
        public RankingDto(double mediaHoras, DesenvolvedorDto desenvolvedor)
        {
            MediaHoras = mediaHoras;
            Desenvolvedor = desenvolvedor;
        }
        public double MediaHoras { get; set; }
        public DesenvolvedorDto Desenvolvedor { get; set; }
    }
}

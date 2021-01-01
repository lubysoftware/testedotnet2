using System;

namespace Tasks.Domain.Developers.Dtos.Ranking
{
    public class DeveloperRankingListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AvgHours { get; set; }
        public int SumHours { get; set; }
    }
}

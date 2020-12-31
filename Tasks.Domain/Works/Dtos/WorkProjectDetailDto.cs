using System;
using Tasks.Domain.Developers.Dtos;

namespace Tasks.Domain.Works.Dtos
{
    public class WorkProjectDetailDto
    {
        public DeveloperListDto Developer { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }
    }
}

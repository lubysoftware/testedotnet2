using System;

namespace Tasks.Domain.Developers.Dtos
{
    public class DetailDeveloperDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string CPF { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace luby_app.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    { 
        DbSet<Domain.Entities.Projeto> Projetos { get; set; }
        DbSet<Domain.Entities.Desenvolvedor> Desenvolvedor { get; set; }
        DbSet<Domain.Entities.DesenvolvedorHora> DesenvolvedorHora { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

using System.Threading.Tasks;
namespace Luby.Domain.Interfaces
{
    public interface IUnitOfWork
    {
         Task Commit();
    }
}
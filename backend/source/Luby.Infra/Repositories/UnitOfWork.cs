using System.Threading.Tasks;
using Luby.Domain.Interfaces;
using Luby.Infra.Context;

namespace Luby.Infra.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly LubyContext _context;

        public UnitOfWork(LubyContext context){
            _context=context;
        }
        
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}